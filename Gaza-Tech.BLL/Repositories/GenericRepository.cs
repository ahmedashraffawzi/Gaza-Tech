using Gaza_Tech.BLL.Interfaces;
using Gaza_Tech.BLL.Specifications;
using Gaza_Tech.DataAccess;
using Gaza_Tech.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gaza_Tech.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet=_context.Set<T>();
        }
        #region Without Specification
        public async Task<IEnumerable<T>> GetAllAsync()
        => await _dbSet.ToListAsync();

        public async Task<T> GetByIdAsync(int id)
            => await _dbSet.FindAsync(id);

        public async Task AddAsync(T item)
            => await _dbSet.AddAsync(item);

        public void Remove(T item)
            => _dbSet.Remove(item);

        public void RemoveRange(IEnumerable<T> items)
            => _dbSet.RemoveRange(items);

        #endregion
        #region With Specification
        public async Task<IEnumerable<T>> GetAllWithSpecificationAsync(ISpecification<T> specification)
            => await ApplySpecification(specification).ToListAsync();

        public async Task<T> GetEntityWithSpecificationAsync(ISpecification<T> specification)
            => await ApplySpecification(specification).FirstOrDefaultAsync();

        private IQueryable<T> ApplySpecification(ISpecification<T> specification)
            => SpecificationEvaluator<T>.GetQuery(_dbSet, specification);

        public  T GetFirstorDefault(Expression<Func<T, bool>>? perdicate = null, string? Includeword = null)
        {
            IQueryable<T> query = _dbSet;
            if (perdicate != null)
            {
                query = query.Where(perdicate);
            }
            if (Includeword != null)
            {
                //_context.Products.Include("Category,Logos,Users)
                foreach (var item in Includeword.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return  query.SingleOrDefault();
        }

		public IEnumerable<T> GetAll(Expression<Func<T, bool>>? perdicate = null, string? Includeword = null)
		{
			IQueryable<T> query = _dbSet;
			if (perdicate != null)
			{
				query = query.Where(perdicate);
			}
			if (Includeword != null)
			{
				//_context.Products.Include("Category,Logos,Users)
				foreach (var item in Includeword.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(item);
				}
			}
			return query.ToList();
		}
		#endregion
	}
}
