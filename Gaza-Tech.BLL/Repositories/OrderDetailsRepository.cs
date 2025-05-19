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
	public class OrderDetailsRepository : GenericRepository<OrderDetails>, IOrderDetailsRepository
	{
		private readonly AppDbContext _dbContext;

		public OrderDetailsRepository(AppDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}

		public void Update(OrderDetails orderDetails)
		{
			_dbContext.OrderDetails.Update(orderDetails);
		}
	}
}
