using Gaza_Tech.BLL.Interfaces;
using Gaza_Tech.DataAccess;
using Gaza_Tech.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaza_Tech.BLL.Repositories
{
    public class ShoppingCartRepository : GenericRepository<UserShoppingCart>, IShoppingCartRepository
    {
        private readonly AppDbContext _dbContext;

        public ShoppingCartRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public int Increase(UserShoppingCart userShoppingCart, int count)
        {
            userShoppingCart.Count += count;
            return userShoppingCart.Count;
        }

        public int Decrease(UserShoppingCart userShoppingCart, int count)
        {
            userShoppingCart.Count -= count;
            return userShoppingCart.Count;
        }
    }
}
