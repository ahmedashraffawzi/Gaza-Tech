using Gaza_Tech.DataAccess.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gaza_Tech.PL.ViewModels
{
	public class ProductViewModel
	{
		public Product Product { get; set; }

		[ValidateNever]
		public IEnumerable<SelectListItem> CategoryList { get; set; }
	}
}
