using Gaza_Tech.BLL.Interfaces;
using Gaza_Tech.BLL.Repositories;
using Gaza_Tech.BLL.Specifications;
using Gaza_Tech.DataAccess.Models;
using Gaza_Tech.PL.ViewModels;
using Gaza_Tech.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.BillingPortal;
using Stripe.Checkout;
using System.Security.Claims;

namespace Gaza_Tech.Areas.Customer.Controllers
{
	[Area("Customer")]
	[Authorize]
	public class CartController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IConfiguration _configuration;
		private const string SessionId = "SessionItems";

		public CartController(IUnitOfWork unitOfWork, IConfiguration configuration)
		{
			_unitOfWork = unitOfWork;
			_configuration = configuration;
		}
		#region Index
		public async Task<IActionResult> Index()
		{
			var userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

			var specification = new ShoppingCartSpecification(userId);

			var shoppingCartVM = new ShoppingCartViewModel
			{
				CartItems = await _unitOfWork.ShoppingCartRepository.GetAllWithSpecificationAsync(specification),
                OrderHeader = new()
               	
            };

			foreach (var cartItem in shoppingCartVM.CartItems)
			{
				shoppingCartVM.TotalPrice += (cartItem.Count * cartItem.Product.Price);
			}

			return View(shoppingCartVM);
		}
		#endregion
		#region Plus
		public async Task<IActionResult> Plus(int itemId)
		{
			var cartItem = await _unitOfWork.ShoppingCartRepository.GetByIdAsync(itemId);

			if (cartItem is null) return NotFound();

			_unitOfWork.ShoppingCartRepository.Increase(cartItem, 1);

			await _unitOfWork.CompleteAsync();

			return RedirectToAction(nameof(Index));
		}
		#endregion
		#region Minus
		public async Task<IActionResult> Minus(int itemId)
		{
			var cartItem = await _unitOfWork.ShoppingCartRepository.GetByIdAsync(itemId);

			if (cartItem is null) return NotFound();

			var userCartSpecification = new ShoppingCartSpecification(cartItem.ApplicationUserId);

			var cart = await _unitOfWork.ShoppingCartRepository.GetAllWithSpecificationAsync(userCartSpecification);

			if (cartItem.Count <= 1)
			{
				_unitOfWork.ShoppingCartRepository.Remove(cartItem);

				HttpContext.Session.SetInt32(Status.SessionKey, cart.Count() - 1);
			}
			else
			{
				_unitOfWork.ShoppingCartRepository.Decrease(cartItem, 1);
			}

			await _unitOfWork.CompleteAsync();

			return RedirectToAction(nameof(Index));
		}
		#endregion
		#region Delete
		public async Task<IActionResult> Delete(int itemId)
		{
			var cartItem = await _unitOfWork.ShoppingCartRepository.GetByIdAsync(itemId);

			if (cartItem is null) return NotFound();

			_unitOfWork.ShoppingCartRepository.Remove(cartItem);

			await _unitOfWork.CompleteAsync();

			var userCartSpecification = new ShoppingCartSpecification(cartItem.ApplicationUserId);

			var cart = await _unitOfWork.ShoppingCartRepository.GetAllWithSpecificationAsync(userCartSpecification);

			HttpContext.Session.SetInt32(Status.SessionKey, cart.Count());

			return RedirectToAction(nameof(Index));
		}
		#endregion
		#region Summary
		[HttpGet]
        //مسؤول عن عرض ملخص الطلب قبل إتمام عملية الشراء.
        public async Task<IActionResult> Summary()
		{
            //الحصول على هوية المستخدم
            var userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
            //جلب محتويات عربة التسوق
            var cartSpecification = new ShoppingCartSpecification(userId);

			var shoppingCartVM = new ShoppingCartViewModel
			{
				CartItems = await _unitOfWork.ShoppingCartRepository.GetAllWithSpecificationAsync(cartSpecification),
				OrderHeader = new()
			};
            //جلب بيانات المستخدم
            var userSpecification = new ApplicationUserSpecification(userId);

			shoppingCartVM.OrderHeader.ApplicationUser = await _unitOfWork.ApplicationUserRepository.GetEntityWithSpecificationAsync(userSpecification);
            //تعبئة بيانات رأس الطلب
            shoppingCartVM.OrderHeader.UserName = shoppingCartVM.OrderHeader.ApplicationUser.FullName;
			shoppingCartVM.OrderHeader.Address = shoppingCartVM.OrderHeader.ApplicationUser.Address;
			shoppingCartVM.OrderHeader.City = shoppingCartVM.OrderHeader.ApplicationUser.City;
			shoppingCartVM.OrderHeader.PhoneNumber = shoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
            //حساب المجموع الكلي
            foreach (var cartItem in shoppingCartVM.CartItems)
			{
				shoppingCartVM.OrderHeader.TotalPrice += (cartItem.Count * cartItem.Product.Price);
			}

			return View(shoppingCartVM);
		}
        #endregion
        #region Checkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        //مسؤول عن معالجة عملية الدفع والشراء 
        public async Task<IActionResult> Checkout(ShoppingCartViewModel shoppingCartViewModel)
        {
            // الحصول على هوية المستخدم
            var userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
            //جلب محتويات عربة التسوق
            var cartSpecification = new ShoppingCartSpecification(userId);

            var shoppingCartVM = new ShoppingCartViewModel
            {
                CartItems = await _unitOfWork.ShoppingCartRepository.GetAllWithSpecificationAsync(cartSpecification),
                OrderHeader = shoppingCartViewModel.OrderHeader,
            };
            //يحدد حالة الطلب وحالة الدفع كـ "معلق"

            //يسجل تاريخ الطلب الحالي
            //يربط الطلب بالمستخدم الحالي
            shoppingCartVM.OrderHeader.OrderStatus = Status.Pending;
            shoppingCartVM.OrderHeader.PaymentStatus = Status.Pending;
            shoppingCartVM.OrderHeader.OrderDate = DateTime.Now;
            shoppingCartVM.OrderHeader.ApplicationUserId = userId;
            //حساب المجموع الكلي
            foreach (var cartItem in shoppingCartVM.CartItems)
            {
                shoppingCartVM.OrderHeader.TotalPrice += (cartItem.Count * cartItem.Product.Price);
            }
            //حفظ الطلب في قاعدة البيانات
            await _unitOfWork.OrderHeaderRepository.AddAsync(shoppingCartVM.OrderHeader);

            await _unitOfWork.CompleteAsync();
            //حفظ تفاصيل الطلب
            foreach (var item in shoppingCartVM.CartItems)
            {
                var orderDetails = new OrderDetails
                {
                    ProductId = item.ProductId,
                    OrderHeaderId = shoppingCartVM.OrderHeader.Id,
                    Price = item.Product.Price,
                    Quantity = item.Count
                };

                await _unitOfWork.OrderDetailsRepository.AddAsync(orderDetails);
                await _unitOfWork.CompleteAsync();
            }

            var domain = "https://localhost:44329/";

            var options = new Stripe.Checkout.SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = $"{domain}customer/cart/orderconfirmation?id={shoppingCartVM.OrderHeader.Id}",
                CancelUrl = $"{domain}customer/cart/index",
            };

            foreach (var item in shoppingCartVM.CartItems)
            {
                var sessionLineItemOptions = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Product.Price * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Name,
                        },
                    },
                    Quantity = item.Count,
                };

                options.LineItems.Add(sessionLineItemOptions);
            }


            var service = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session = service.Create(options);
            shoppingCartVM.OrderHeader.SessionId = session.Id;
            shoppingCartVM.OrderHeader.PaymentIntentId = session.PaymentIntentId;

            await _unitOfWork.CompleteAsync();

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
        #endregion
        #region OrderConfirmation
        public async Task<IActionResult> OrderConfirmation(int id)
        {
            var orderHeader = await _unitOfWork.OrderHeaderRepository.GetByIdAsync(id);

            var service = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session = service.Get(orderHeader.SessionId);

            if (session.PaymentStatus.ToLower() == "paid")
            {
                _unitOfWork.OrderHeaderRepository.UpdateOrderStatus(id, Status.Approve);
                _unitOfWork.OrderHeaderRepository.UpdatePaymentStatus(id, Status.Approve);
                orderHeader.PaymentIntentId = session.PaymentIntentId;
                await _unitOfWork.CompleteAsync();
            }

            var cartSpecification = new ShoppingCartSpecification(orderHeader.ApplicationUserId);

            var shoppingCarts = await _unitOfWork.ShoppingCartRepository.GetAllWithSpecificationAsync(cartSpecification);

            _unitOfWork.ShoppingCartRepository.RemoveRange(shoppingCarts);

            await _unitOfWork.CompleteAsync();

            HttpContext.Session.Clear();

            return View(id);
            //return RedirectToAction("Details", "CustomerOrder", new { orderId = id });
        }

        #endregion
    }
}
