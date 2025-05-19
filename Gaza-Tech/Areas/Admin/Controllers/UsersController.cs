using Gaza_Tech.DataAccess;
using Gaza_Tech.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace Gaza_Tech.Areas.Admin.Controllers
{
  [Authorize(Roles = UsersRoles.AdminRole)]
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly AppDbContext _dbContext;
        public UsersController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #region Index
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetUsersData()
        {
            var userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

            var usersExceptCurrent = _dbContext.ApplicationUsers.Where(user => user.Id != userId).ToList();

            return Json(new { data = usersExceptCurrent });
        }
		#endregion
		#region Lockout
		public IActionResult LockUnlock(string? id)
		{
			var user = _dbContext.ApplicationUsers.FirstOrDefault(u => u.Id == id);

			if (user is null) return NotFound();

			user.LockoutEnd = (user.LockoutEnd is null || user.LockoutEnd < DateTime.Now) ? DateTime.Now.AddMonths(1) : DateTime.Now;

			_dbContext.SaveChanges();

			return RedirectToAction("Index", "Users", new { area = "Admin" });
		}
		#endregion
	}
}
