using System.Linq.Expressions;

namespace iscaslune.Api.Infrastructure.Filtros.Filtros;

public static class FilterOrderBy
{
    public static IQueryable<TEntity> OrderByProperty<TEntity>(
            this IQueryable<TEntity> source,
            string propertyName,
            bool ascending = true)
    {
        var entityType = typeof(TEntity);
        var propertyInfo = entityType.GetProperty(propertyName);

        if (propertyInfo == null)
            throw new ArgumentException($"Property {propertyName} not found on type {entityType.Name}");


        var parameter = Expression.Parameter(entityType, "x");
        var propertyAccess = Expression.MakeMemberAccess(parameter, propertyInfo);
        var orderByExp = Expression.Lambda(propertyAccess, parameter);

        var methodName = ascending ? "OrderBy" : "OrderByDescending";
        var resultExp = Expression.Call(
            typeof(Queryable),
            methodName,
            new[] { entityType, propertyInfo.PropertyType },
            source.Expression,
            Expression.Quote(orderByExp));

        return source.Provider.CreateQuery<TEntity>(resultExp);
    }
}
