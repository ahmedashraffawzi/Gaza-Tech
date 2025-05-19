using Gaza_Tech.BLL.Specifications;
using Gaza_Tech.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaza_Tech.BLL.Repositories
{
	public class ApplicationUserSpecification : BaseSpecification<ApplicationUser>
	{
		public ApplicationUserSpecification() : base() { }

		public ApplicationUserSpecification(string id) : base(u => u.Id == id) { }
	}
}
