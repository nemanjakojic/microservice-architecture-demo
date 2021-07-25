using Demo.Microservice.Core.Common.Enum;
using Demo.Microservice.Core.Common.Model;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Demo.Microservice.Core.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> SortAndPage<T>(this IQueryable<T> queryable, QueryFilter filter)
        {
            return Page(Sort(queryable, filter), filter);
        }

        public static IQueryable<T> Page<T>(this IQueryable<T> queryable, QueryFilter queryFilter)
        {
            if (queryFilter == null)
            {
                return queryable;
            }

            if (queryFilter.Page <= 0)
            {
                throw new ArgumentOutOfRangeException("Page number must be greater than zero.");
            }

            if (queryFilter.PageSize <= 0)
            {
                throw new ArgumentOutOfRangeException("Page size must be greater than zero.");
            }

            int offset = (queryFilter.Page - 1) * queryFilter.PageSize;
            return queryable.Skip(offset).Take(queryFilter.PageSize);
        }

        public static IQueryable<T> Sort<T>(this IQueryable<T> queryable, QueryFilter queryFilter)
        {
            if (queryFilter?.Sort == null)
            {
                return queryable;
            }

            if (string.IsNullOrWhiteSpace(queryFilter.Sort.Field))
            {
                throw new ArgumentNullException("QueryFilter.Sort.Field");
            }

            var sortParam = Expression.Parameter(typeof(T), $"orderBy{queryFilter.Sort.Field}");
            var sortProperty = Expression.Convert(Expression.Property(sortParam, queryFilter.Sort.Field), typeof(object));
            var sortExpression = Expression.Lambda<Func<T, object>>(sortProperty, sortParam);

            return queryFilter.Sort.Order switch
            {
                Order.Asc => queryable.OrderBy(sortExpression),
                Order.Desc => queryable.OrderByDescending(sortExpression),
                _ => throw new ArgumentException($"Invalid QueryFilter.Sort.Order: { queryFilter.Sort.Order }.")
            };
        }
    }
}
