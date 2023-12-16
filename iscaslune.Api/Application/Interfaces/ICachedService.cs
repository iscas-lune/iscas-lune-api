namespace iscaslune.Api.Application.Interfaces;

public interface ICachedService<T> where T : class
{
    Task<T?> GetItemAsync(string key);
    Task<List<T>?> GetListItemAsync(string key);
    Task SetListItemAsync(string key, List<T> itens);
    Task SetItemAsync(string key, T item);
    Task RemoveCachedAsync(string key);
}
