using Gaza_Tech.BLL.Interfaces;
using Gaza_Tech.DataAccess;
using Gaza_Tech.DataAccess.Models;
using Gaza_Tech.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gaza_Tech.Areas.Admin.Controllers
{
    [Authorize(Roles = UsersRoles.AdminRole)]

    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
		public async Task<IActionResult> GetCategoriesData()
		{
			var categories = await _unitOfWork.CategoryRepository.GetAllAsync();

			return Json(new { data = categories });
		}
		#region Details
		public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null) return BadRequest();

            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id.Value);

            return category is null ? NotFound() : View(ViewName, category);
        }
        #endregion
        #region Index
        public async Task<IActionResult> Index()
        {
            var Categoryies = await _unitOfWork.CategoryRepository.GetAllAsync();
            return View(Categoryies);
        }
        #endregion
        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                //_context.Categories.Add(category);
                await _unitOfWork.CategoryRepository.AddAsync(category);
                //_context.SaveChanges();
                await _unitOfWork.CompleteAsync();
                TempData["Create"] = "Item has Created Successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }
        #endregion
        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid) // Server-Side Validation
            {
                _unitOfWork.CategoryRepository.UpdateAsync(category);

                int result = await _unitOfWork.CompleteAsync();

                if (result > 0) TempData["Update"] = "Category Is Updated Successfully";

                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        #endregion
        #region Delete
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var categoryInDb = await _unitOfWork.CategoryRepository.GetByIdAsync(id);

            if (categoryInDb is null)
                return Json(new { success = false, message = "Error While Deleting Category" });

            _unitOfWork.CategoryRepository.Remove(categoryInDb);

            await _unitOfWork.CompleteAsync();

            return Json(new { success = true, message = "Category Is Deleted Successfully" });
        }

        #endregion
    }
}
