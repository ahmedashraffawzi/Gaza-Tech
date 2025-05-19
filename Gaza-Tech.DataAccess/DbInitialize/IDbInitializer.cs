using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaza_Tech.DataAccess.DbInitialize
{
	public interface IDbInitializer
	{
		public Task InitializeAsync();
	}
}
