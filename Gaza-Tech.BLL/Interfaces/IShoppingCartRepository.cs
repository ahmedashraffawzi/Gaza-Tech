using Gaza_Tech.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaza_Tech.BLL.Interfaces
{
    public interface IShoppingCartRepository : IGenericRepository<UserShoppingCart>
    {
        int Increase(UserShoppingCart userShoppingCart, int count);
        int Decrease(UserShoppingCart userShoppingCart, int count);
    }
}
