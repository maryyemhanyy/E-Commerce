using E_Commerce.Domain.Common;
using E_Commerce.Domain.Interfaces;
using System.Linq.Expressions;

namespace E_Commerce.API.Specifications
{
    public class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        public Expression<Func<TEntity, object>> Orderby { get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderbyExpression)
        {
            Orderby = orderbyExpression;
        }
        public Expression<Func<TEntity, object>> OrderbyDescending { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        protected void ApplyPaging(int pageSize, int pageIndex)
        {
            IsPagingEnabled = true;
            Take = pageSize;
            Skip = (pageIndex - 1) * pageSize;          
        }

        protected void AddOrderByDesc(Expression<Func<TEntity, object>> orderbyDescExpression)
        {
            OrderbyDescending = orderbyDescExpression;
        }

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
