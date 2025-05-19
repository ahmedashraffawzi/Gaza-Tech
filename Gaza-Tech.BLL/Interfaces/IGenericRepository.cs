using Gaza_Tech.BLL.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gaza_Tech.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        #region Without Specification
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T item);
        void Remove(T item);
        void RemoveRange(IEnumerable<T> items);

		#endregion
		#region With Specification
		IEnumerable<T> GetAll(Expression<Func<T, bool>>? perdicate = null, string? Includeword = null);
		Task<IEnumerable<T>> GetAllWithSpecificationAsync(ISpecification<T> specification);
        Task<T> GetEntityWithSpecificationAsync(ISpecification<T> specification);
       T GetFirstorDefault(Expression<Func<T, bool>>? perdicate = null, string? Includeword = null);
        #endregion

    }
}
