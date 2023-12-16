using iscaslune.Api.Infrastructure.Filters.Model;
using System.Linq.Expressions;

namespace iscaslune.Api.Infrastructure.Filtros.Filtros;

public static class FilterSearch
{
    public static IQueryable<TEntity> Search<TEntity>(this IQueryable<TEntity> result, string propertyName, string? search = "")
    {
        if (string.IsNullOrWhiteSpace(search) || string.IsNullOrWhiteSpace(propertyName))
            return result;

        var type = typeof(TEntity);
        var parameter = Expression.Parameter(type, "x");
        var property = Expression.Property(parameter, propertyName);
        var toLowerMethod = FilterIQueryableCustom.GetType(property.Type)
            ?? throw new InvalidOperationException("ToLower method not found");
        var callToLower = Expression.Call(property, toLowerMethod);

        var searchExpression = Expression.Constant(search.ToLower(), typeof(string));
        var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) })
                             ?? throw new InvalidOperationException("Contains method not found");

        var containsCall = Expression.Call(callToLower, containsMethod, searchExpression);

        var lambda = Expression.Lambda<Func<TEntity, bool>>(containsCall, parameter);

        return result.Where(lambda);
    }

    public static IQueryable<TEntity> SearchList<TEntity>(this IQueryable<TEntity> result, List<FilterSearchModel>? filterSearch)
    {
        if (filterSearch == null || filterSearch.Count == 0) return result;
        if (filterSearch.Count == 1)
        {
            var filter = filterSearch.FirstOrDefault();
            if (filter == null) return result;

            return result.Search(filter.PropetyName, filter.Search);
        }

        var type = typeof(TEntity);
        var parameter = Expression.Parameter(type, "x");
        var orExpressions = new List<Expression>();

        foreach (var item in filterSearch)
        {
            var property = Expression.Property(parameter, item.PropetyName);

            var toLowerMethod = FilterIQueryableCustom.GetType(property.Type)
            ?? throw new InvalidOperationException("ToLower method not found");
            var callToLower = Expression.Call(property, toLowerMethod);

            var searchExpression = Expression.Constant(item.Search.ToLower(), typeof(string));
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) })
                                 ?? throw new InvalidOperationException("Contains method not found");

            var containsCall = Expression.Call(callToLower, containsMethod, searchExpression);

            orExpressions.Add(containsCall);
        }

        Expression orExpression = orExpressions.Aggregate(Expression.OrElse);

        var lambda = Expression.Lambda<Func<TEntity, bool>>(orExpression, parameter);

        return result.Where(lambda);
    }
}
