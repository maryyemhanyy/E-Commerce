using E_Commerce.Domain.Common;
using E_Commerce.Domain.Interfaces;
using System.Linq.Expressions;

namespace E_Commerce.API.Specifications
{
    public class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        protected BaseSpecifications(Expression<Func<TEntity, bool>>? criteria = null)
        {
            Criteria = criteria;
        }

        protected void AddInclude(Expression<Func<TEntity, object>> include)
        {
            IncludeExpressions.Add(include);
        }
    }
}
