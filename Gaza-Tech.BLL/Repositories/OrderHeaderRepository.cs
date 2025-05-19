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
	public class OrderHeaderRepository : GenericRepository<OrderHeader>, IOrderHeaderRepository
	{
		private readonly AppDbContext _dbContext;

		public OrderHeaderRepository(AppDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}

		public void Update(OrderHeader orderHeader)
		{
			_dbContext.OrderHeaders.Update(orderHeader);
		}

		public void UpdateOrderStatus(int id, string orderStatus)
		{
			var orderHeaderFromDb = _dbContext.OrderHeaders.FirstOrDefault(h => h.Id == id);

			if (orderHeaderFromDb != null)
			{
				orderHeaderFromDb.OrderStatus = orderStatus;
				orderHeaderFromDb.PaymentDate = DateTime.Now;
			}
		}

		public void UpdatePaymentStatus(int id, string paymentStatus)
		{
			var orderHeaderFromDb = _dbContext.OrderHeaders.FirstOrDefault(h => h.Id == id);

			if (orderHeaderFromDb != null)
				orderHeaderFromDb.PaymentStatus = paymentStatus;
		}
	}
}
