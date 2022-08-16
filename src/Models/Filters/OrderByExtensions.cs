using System.Linq.Expressions;

namespace Models.Filters
{
    public static class OrderByExtensions
    {
        public static IOrderedQueryable<T> OrderByProperty<T>(this IQueryable<T> q, string SortField, bool Ascending)
        {
            var parameter = Expression.Parameter(typeof(T), "p");
            var property = Expression.Property(parameter, SortField);
            var lambda = Expression.Lambda(property, parameter);

            string method = Ascending ? "OrderBy" : "OrderByDescending";

            Type[] types = new Type[] { q.ElementType, lambda.Body.Type };

            var expression = Expression.Call(typeof(Queryable), method, types, q.Expression, lambda);

            return (IOrderedQueryable<T>)q.Provider.CreateQuery<T>(expression);
        }
    }
}
