using Gaza_Tech.BLL.Interfaces;
using Gaza_Tech.BLL.Specifications;
using Gaza_Tech.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gaza_Tech.Components
{
    public class CartViewComponent : ViewComponent
    {
            private readonly IUnitOfWork _unitOfWork;

            public CartViewComponent(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IViewComponentResult> InvokeAsync()
            {
                var userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);

                if (userId != null)
                {
                    if (HttpContext.Session.GetInt32(Status.SessionKey) != null)
                    {
                        return View(HttpContext.Session.GetInt32(Status.SessionKey));
                    }
                    else
                    {
                        var userCartSpecification = new ShoppingCartSpecification(userId.Value);

                        var cart = await _unitOfWork.ShoppingCartRepository.GetAllWithSpecificationAsync(userCartSpecification);

                        HttpContext.Session.SetInt32(Status.SessionKey, cart.Count());

                        return View(HttpContext.Session.GetInt32(Status.SessionKey));
                    }
                }
                else
                {
                    HttpContext.Session.Clear();
                    return View(0);
                }
            }
        }
    }

