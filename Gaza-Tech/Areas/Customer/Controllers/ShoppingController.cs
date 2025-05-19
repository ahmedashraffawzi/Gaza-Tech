using Gaza_Tech.BLL.Interfaces;
using Gaza_Tech.BLL.Specifications;
using Gaza_Tech.DataAccess.Models;
using Gaza_Tech.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Drawing2D;
using System.Security.Claims;
using X.PagedList.Extensions;

namespace Gaza_Tech.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ShoppingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public ShoppingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
       
		#region Index
		[HttpGet]
        public async Task<IActionResult> Index(int? page, string searchString,string brand,int? categoryId)
        {
            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)

            var specification = new ProductSpecification();

            var products = await _unitOfWork.ProductRepository.GetAllWithSpecificationAsync(specification);
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }
			if (!string.IsNullOrEmpty(brand))
			{
				products = products.Where(p => p.Brand.Contains(brand, StringComparison.OrdinalIgnoreCase)).ToList();
			}
            if (categoryId.HasValue && categoryId != 0) // تأكد أن الفئة ليست فارغة
            {
                products = products.Where(p => p.CategoryId == categoryId.Value).ToList();
            }
            var onePageOfProducts = products.ToPagedList(pageNumber, 12); // will only contain 10 products max because of the pageSize
			ViewBag.Categories = await _unitOfWork.CategoryRepository.GetAllAsync();
			return View(onePageOfProducts);
        }
        #endregion
        #region Details
        [HttpGet]
        public async Task<IActionResult> Details([FromRoute(Name = "id")] int? productId)
        {
            if (productId is null) return BadRequest();

            var specification = new ProductSpecification(productId.Value);

            var product = await _unitOfWork.ProductRepository.GetEntityWithSpecificationAsync(specification);

            if (product is null) return NotFound();

            var userCart = new UserShoppingCart()
            {
                ProductId = productId.Value,
                Product = product,
                Count = 1
            };

            return View(userCart);
        }

        #endregion
        #region AddToCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddToCart([FromForm] UserShoppingCart shoppingCart)
        {
            if (shoppingCart == null) return BadRequest();

            var userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
            //يربط المنتج بالمستخدم الحالي
            shoppingCart.ApplicationUserId = userId;

            var cartItemSpecification = new ShoppingCartSpecification(userId, shoppingCart.ProductId);
            //للبحث عن المنتج في عربة المستخدم
            var userCartItem = await _unitOfWork.ShoppingCartRepository.GetEntityWithSpecificationAsync(cartItemSpecification);
            // إذا لم يكن المنتج موجودًا في العربه

            if (userCartItem is null)
            {
                await _unitOfWork.ShoppingCartRepository.AddAsync(shoppingCart);

                await _unitOfWork.CompleteAsync();

                var userCartSpecification = new ShoppingCartSpecification(userId);

                var cart = await _unitOfWork.ShoppingCartRepository.GetAllWithSpecificationAsync(userCartSpecification);
                //يحسب عدد العناصر في العربة ويخزنه في الـ Session
                HttpContext.Session.SetInt32(Status.SessionKey, cart.Count());
                await _unitOfWork.CompleteAsync();
            }
            else
            {
                //يزيد كمية المنتج فقط
                _unitOfWork.ShoppingCartRepository.Increase(userCartItem, shoppingCart.Count);

                await _unitOfWork.CompleteAsync();
            }

            //await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(Index));
        }
        #endregion

    }
}
