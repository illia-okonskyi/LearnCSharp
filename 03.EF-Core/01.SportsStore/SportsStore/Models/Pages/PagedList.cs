using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace SportsStore.Models.Pages
{
    public class PagedList<T> : List<T>
    {
        public PagedList(IQueryable<T> query, QueryOptions options = null)
        {
            CurrentPage = options.CurrentPage;
            PageSize = options.PageSize;
            Options = options;
            if (options != null)
            {
                if (!string.IsNullOrEmpty(options.OrderPropertyName))
                {
                    query = Order(query, options.OrderPropertyName, options.DescendingOrder);
                }
                if (!string.IsNullOrEmpty(options.SearchPropertyName) && !string.IsNullOrEmpty(options.SearchTerm))
                {
                    query = Search(query, options.SearchPropertyName, options.SearchTerm);
                }
            }

            TotalPages = query.Count() / PageSize;
            AddRange(query.Skip((CurrentPage - 1) * PageSize).Take(PageSize));
        }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public QueryOptions Options { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        private static IQueryable<T> Search(IQueryable<T> query, string propertyName, string searchTerm)
        {
            // Declare T x
            var parameter = Expression.Parameter(typeof(T), "x");
            // Access x.fullPropertyPath: e.g. x.outerProp.innerProp
            var source = propertyName.Split('.').Aggregate(seed: (Expression)parameter, func: Expression.Property);
            // (string) x.fullPropertyPath.Contains(searchTerm)
            var body = Expression.Call(source,
                "Contains",
                Type.EmptyTypes,
                Expression.Constant(searchTerm, typeof(string)));
            // Build lambda expression: bool(T) x => x.fullPropertyPath.Contains(searchTerm) 
            var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);
            // Apply lambda
            return query.Where(lambda);
        }

        private static IQueryable<T> Order(IQueryable<T> query, string propertyName, bool desc)
        {
            // Declare T x
            var parameter = Expression.Parameter(typeof(T), "x");
            // Access x.fullPropertyPath: e.g. x.outerProp.innerProp
            var source = propertyName.Split('.').Aggregate((Expression)parameter, Expression.Property);
            // Build lambda expression: Type(x.fullPropertyPath)(T) x => x.fullPropertyPath
            var lambda = Expression.Lambda(typeof(Func<,>).MakeGenericType(typeof(T), source.Type), source, parameter);
            var orderMethodName = desc ? "OrderByDescending" : "OrderBy";
            // Select order method: Order<T, Type(x.fullPropertyPath)>
            var orderMethod = typeof(Queryable).GetMethods().Single(m => {
                return m.Name == orderMethodName &&
                    m.IsGenericMethodDefinition &&
                    m.GetGenericArguments().Length == 2 &&
                    m.GetParameters().Length == 2;
            }).MakeGenericMethod(typeof(T), source.Type);
                
            // Invoke order extension method (no object is specified and target object is passed
            // as argument instead)
            return orderMethod.Invoke(null, new object[] { query, lambda }) as IQueryable<T>;
        }
    }
}
