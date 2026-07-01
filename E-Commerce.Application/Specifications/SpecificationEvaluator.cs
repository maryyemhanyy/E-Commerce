using E_Commerce.Domain.Common;
using E_Commerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specifications
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity>CreateQuery<TEntity , TKey>(IQueryable<TEntity> InputQuery , ISpecifications<TEntity , TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            var query = InputQuery;

            if(specifications.Criteria != null)
            {
                query = query.Where(specifications.Criteria);
            }

            if (specifications.IncludeExpressions.Any())
            {
               query = specifications.IncludeExpressions.Aggregate(query, (current, nextIncludeExpression) => current.Include(nextIncludeExpression));
            }
            return query;
        }
    }
}
