using Article.API.Domain;
using Article.API.Domain.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace Article.API.Infrastructure.Extensions
{
    public static  class CommonExtensions
    {
        public static IQueryable<TEntity> SkipTake<TEntity>(this IQueryable<TEntity> query, int index, int size) where TEntity : IEntity
        {
            return query.Skip((index - 1) * size).Take(size);
        }
        
        public static int CalculatePageCount(this IQueryable<IEntity> query, int? size)
        {
            return (int)Math.Ceiling(query.Count() / (double)size);
        }
        
        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> query, ICollection<Ordination> ordination) where TEntity : IEntity
        {
            StringBuilder builder = new StringBuilder();
            if (ordination != null)
            {
                foreach (var item in ordination)
                {
                    if (builder.Length != 0)
                    {
                        builder.Append(",");
                    }

                    builder.Append(item.Value);
                    if (item.IsAscending)
                    {
                        builder.Append(" ASC");
                    }
                    else
                    {
                        builder.Append(" DESC");
                    }
                }
            }

            return string.IsNullOrEmpty(builder.ToString()) ? query : query.AsQueryable().OrderBy(builder.ToString());
        }
        
        public static void AddParameter(this IHeaderDictionary headers, HttpRequest request, string key, List<Ordination> ordination, int pageSize, int pageIndex)
        {
            var builder = new StringBuilder();

            builder.Append("?");
            if (ordination != null)
            {
                for (int i = 0; i < ordination.Count; i++)
                {
                    if (builder.Length > 1)
                        builder.Append("&");

                    var value = ordination[i].Value;
                    var isAscending = ordination[i].IsAscending;

                    builder.Append(nameof(ordination)).Append("[").Append(i).Append("]").Append(".")
                           .Append(nameof(value)).Append("=").Append(value)
                           .Append("&")
                           .Append(nameof(ordination)).Append("[").Append(i).Append("]").Append(".")
                           .Append(nameof(isAscending)).Append("=").Append(isAscending.ToString());
                }
            }

            if (builder.Length > 1)
                builder.Append("&");

            var headerParameterLink = builder.Append(nameof(pageSize)).Append("=").Append(pageSize)
                                      .Append("&")
                                      .Append(nameof(pageIndex)).Append("=").Append(pageIndex)
                                      .ToString();

            headerParameterLink = $"{request.Scheme}://{request.Host}{request.Path}{headerParameterLink}";
            headers.Add(key, headerParameterLink);
        }
        
        public static IQueryable<T> WhereBy<T>(this IQueryable<T> source, string filterQuery)
        {
            if (string.IsNullOrEmpty(filterQuery))
                return source;

            source = source.Where(filterQuery);

            return source;
        }
    }
}
