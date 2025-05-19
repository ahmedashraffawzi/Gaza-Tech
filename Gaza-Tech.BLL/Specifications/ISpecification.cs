using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gaza_Tech.BLL.Specifications
{
    public interface ISpecification<T> where T : class
    {
        // Signature For Properity For Where Condition
        public Expression<Func<T, bool>> Criteria { get; set; }

        // Signature For Properity For List Of Includes Expressions
        public List<Expression<Func<T, object>>> Includes { get; set; }
        public Expression<Func<T,object>> OrderBy { get; }
        public Expression<Func<T,object>> OrderByDescending { get; }
    }
}
