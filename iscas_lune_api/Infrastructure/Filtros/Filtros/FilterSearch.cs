using iscaslune.Api.Infrastructure.Filters.Model;
using System.Linq.Expressions;

namespace iscaslune.Api.Infrastructure.Filtros.Filtros;

public static class FilterSearch
{
    public static IQueryable<TEntity> WhereIsNotNull<TEntity>(this IQueryable<TEntity> querable, object? obj, Expression<Func<TEntity, bool>> where)
    {
        if (obj != null)
            querable = querable.Where(where);

        return querable;
    }
}
