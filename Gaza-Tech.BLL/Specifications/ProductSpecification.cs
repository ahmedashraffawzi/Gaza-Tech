﻿using Gaza_Tech.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaza_Tech.BLL.Specifications
{
	public class ProductSpecification : BaseSpecification<Product>
	{
		public ProductSpecification() : base()
		{
			AddIncludes(p => p.Category);
		}

		public ProductSpecification(int id) : base(p => p.Id == id)
		{
			AddIncludes(p => p.Category);
		}
	}
}
