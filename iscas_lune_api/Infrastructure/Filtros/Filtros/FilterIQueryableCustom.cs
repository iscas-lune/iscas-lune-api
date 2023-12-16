using iscaslune.Api.Infrastructure.Filters.Model;
using System.Reflection;

namespace iscaslune.Api.Infrastructure.Filtros.Filtros;

public static class FilterIQueryableCustom
{
    public static MethodInfo? GetType(Type type)
    {
        return type == typeof(string) ? typeof(string).GetMethod("ToLower", Type.EmptyTypes)
                               : type == typeof(int) ? typeof(int).GetMethod("ToString", Type.EmptyTypes)
                               : type == typeof(DateTime) ? typeof(DateTime).GetMethod("ToString", Type.EmptyTypes)
                               : throw new InvalidOperationException("ToLower method not found");
    }

    public static IQueryable<TEntity> FilterAll<TEntity>(this IQueryable<TEntity> result, FilterModel filterModel)
    {

        List<FilterSearchModel> filterSearch = new();

        foreach (var item in filterModel.GetType().GetProperties())
        {
            var value = item.GetValue(filterModel);
            if (item.Name != "OrderBy" &&
                item.Name != "Asc" &&
                value != null)
            {
                var propetyName = item.Name;
                filterSearch.Add(new() { PropetyName = propetyName, Search = value.ToString() ?? "" });
            }
        }

        return result
            .SearchList(filterSearch)
            .OrderByProperty(filterModel.OrderBy, filterModel.Asc);
    }
}
