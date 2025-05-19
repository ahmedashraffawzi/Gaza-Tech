using Gaza_Tech.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaza_Tech.BLL.Interfaces
{
	public interface IOrderHeaderRepository : IGenericRepository<OrderHeader>
	{
		public void Update(OrderHeader orderHeader);
		public void UpdateOrderStatus(int id, string orderStatus);
		public void UpdatePaymentStatus(int id, string paymentStatus);
	}
}
