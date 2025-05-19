using Microsoft.EntityFrameworkCore;
using Gaza_Tech.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gaza_Tech.DataAccess.Models;
using Gaza_Tech.DataAccess;

namespace Gaza_Tech.BLL.Repositories
{
	public class ProductRepository : GenericRepository<Product>, IProductRepository
	{
		private readonly AppDbContext _dbContext;

		public ProductRepository(AppDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}

		public void UpdateAsync(Product updatedProduct)
		{
			var product = _dbContext.Products.FirstOrDefault(c => c.Id == updatedProduct.Id);

			if (product != null)
			{
				product.Name = updatedProduct.Name;
				product.Description = updatedProduct.Description;
				product.Brand= updatedProduct.Brand;
				product.Price = updatedProduct.Price;
				product.Image = updatedProduct.Image;
				product.CategoryId = updatedProduct.CategoryId;
			}
		}
	}
}
