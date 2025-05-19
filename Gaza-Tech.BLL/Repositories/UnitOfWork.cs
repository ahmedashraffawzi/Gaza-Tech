using Gaza_Tech.BLL.Interfaces;
using Gaza_Tech.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaza_Tech.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _Context;

        public ICategoryRepository CategoryRepository { get; set;}
        public IProductRepository ProductRepository { get; set;}
        public IShoppingCartRepository ShoppingCartRepository { get; set; }
        public IOrderHeaderRepository OrderHeaderRepository { get; set; }
        public IOrderDetailsRepository OrderDetailsRepository { get; set; }
        public IApplicationUserRepository ApplicationUserRepository { get; set; }
        public UnitOfWork(AppDbContext Context)
        {
            _Context = Context;
            CategoryRepository = new CategoryRepository(Context);
            ProductRepository = new ProductRepository(Context);
            ShoppingCartRepository=new ShoppingCartRepository(Context);
            OrderHeaderRepository= new OrderHeaderRepository(Context);
            OrderDetailsRepository= new OrderDetailsRepository(Context);
            ApplicationUserRepository= new ApplicationUserRepository(Context);
        }
        public async Task<int> CompleteAsync()
        {
          return  await _Context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _Context.Dispose();
        }
    }
}
