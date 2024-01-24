using Microsoft.EntityFrameworkCore;

namespace iscas_lune_api.Infrastructure.Filtros.Filtros;

public static class TotalPageFilter
{
    public static async Task<int> TotalPage<TEntity>(this IQueryable<TEntity> queryable, int take)
    {
        var count = await queryable.CountAsync();
        var totalPages = (int)Math.Ceiling((decimal)count / take);
        return totalPages;
    }
}
