using Gaza_Tech.BLL.Interfaces;
using Gaza_Tech.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gaza_Tech.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = UsersRoles.AdminRole)]
	public class DashboardController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
        public DashboardController(IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
		}
		#region Index
		public async Task<IActionResult> Index()
		{
			var orders = await _unitOfWork.OrderHeaderRepository.GetAllAsync();

			ViewBag.Orders = orders.Count();

			ViewBag.ApprovedOrders = orders.Where(o => o.OrderStatus == Status.Approve).Count();

			var currentUserId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

			ViewBag.Users = (await _unitOfWork.ApplicationUserRepository.GetAllAsync()).Where(x => x.Id != currentUserId).Count();

			ViewBag.Products = (await _unitOfWork.ProductRepository.GetAllAsync()).Count();

			return View();
		}
		#endregion

	}
}
