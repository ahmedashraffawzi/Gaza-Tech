using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gaza_Tech.BLL.Specifications
{
    public class BaseSpecification<T>:ISpecification<T> where T : class
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();

		public Expression<Func<T, object>> OrderBy { get; private set; }

		public Expression<Func<T, object>> OrderByDescending { get; private set; }

		public BaseSpecification()
        {
        }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        protected void AddIncludes(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
		protected void AddOrderBy(Expression<Func<T, object>> orderByexpression)
		{
			OrderBy=orderByexpression;
		}
		protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescexpression)
		{
			OrderByDescending = orderByDescexpression;
		}
	}
}
