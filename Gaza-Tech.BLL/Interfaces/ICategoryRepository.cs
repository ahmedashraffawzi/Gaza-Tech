using Gaza_Tech.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaza_Tech.BLL.Interfaces
{
    public interface ICategoryRepository:IGenericRepository<Category>
    {
        void UpdateAsync(Category updatedCategory);
    }
}
