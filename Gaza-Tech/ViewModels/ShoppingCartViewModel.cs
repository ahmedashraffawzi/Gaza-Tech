using Gaza_Tech.DataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace Gaza_Tech.PL.ViewModels
{
	public class ShoppingCartViewModel
	{
		public IEnumerable<UserShoppingCart> CartItems { get; set; }
		public decimal TotalPrice { get; set; }
		public OrderHeader OrderHeader { get; set; }
	}
}
