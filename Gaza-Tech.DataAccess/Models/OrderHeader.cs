using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaza_Tech.DataAccess.Models
{
	public class OrderHeader
	{
		public int Id { get; set; }

		public string ApplicationUserId { get; set; }

		[ValidateNever]
		[ForeignKey("ApplicationUserId")]
		public ApplicationUser ApplicationUser { get; set; }

        #region UserData
        [Required(ErrorMessage = "Name is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        public string PhoneNumber { get; set; }
		#endregion

		public DateTime OrderDate { get; set; }

		public DateTime ShippingDate { get; set; }

		public string? OrderStatus { get; set; }

		public string? PaymentStatus { get; set; }

		public decimal TotalPrice { get; set; }

		public string? TrackingNumber { get; set; }

		public string? Carrier { get; set; }

		public DateTime PaymentDate { get; set; }

		#region Stripe Props
		public string? SessionId { get; set; }
		public string? PaymentIntentId { get; set; }
		#endregion
	}
}
