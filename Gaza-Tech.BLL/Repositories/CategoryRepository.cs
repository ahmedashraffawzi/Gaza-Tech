using Gaza_Tech.BLL.Interfaces;
using Gaza_Tech.DataAccess;
using Gaza_Tech.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaza_Tech.BLL.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _Context;

        public CategoryRepository(AppDbContext Context) : base(Context)
        {
            _Context = Context;
        }
        public void UpdateAsync(Category updatedCategory)
        {
            var category = _Context.Categories.FirstOrDefault(c => c.Id == updatedCategory.Id);

            if (category != null)
            {
                category.Name = updatedCategory.Name;
                category.Description = updatedCategory.Description;
                category.CreatedTime = DateTime.Now;
            }
        }
    }
}
