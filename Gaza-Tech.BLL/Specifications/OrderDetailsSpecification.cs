using Gaza_Tech.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaza_Tech.BLL.Specifications
{
	public class OrderDetailsSpecification : BaseSpecification<OrderDetails>
	{
		public OrderDetailsSpecification(int orderHeaderId) : base(d => d.OrderHeaderId == orderHeaderId)
		{
			AddIncludes(d => d.Product);
		}
	}
}
