using iscaslune.Api.Application.Interfaces;
using iscaslune.Api.Validations;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace iscaslune.Api.Application.Services;

public class CachedService<T> : ICachedService<T> where T : class
{
    private readonly IDistributedCache _distributedCache;
    private readonly DistributedCacheEntryOptions _options;
    private readonly JsonSerializerOptions _serializerOptions;
    public CachedService(IDistributedCache distributedCache)
    {
        _serializerOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = ReferenceHandler.Preserve
        };
        _options = new DistributedCacheEntryOptions()
                      .SetAbsoluteExpiration(TimeSpan.FromMinutes(60))
                      .SetSlidingExpiration(TimeSpan.FromMinutes(30));

        _distributedCache = distributedCache;
    }

    public async Task<T?> GetItemAsync(string key)
    {
        Valid.ValidStringSemLength(key);
        var value = await _distributedCache.GetStringAsync(key);
        return value is null ? null : JsonSerializer.Deserialize<T>(value, _serializerOptions);
    }

    public async Task<List<T>?> GetListItemAsync(string key)
    {
        Valid.ValidStringSemLength(key);
        var values = await _distributedCache.GetStringAsync(key);
        return values is null ? null : JsonSerializer.Deserialize<List<T>>(values, _serializerOptions);
    }

    public async Task RemoveCachedAsync(string key)
    {
        Valid.ValidStringSemLength(key);
        await _distributedCache.RemoveAsync(key);
    }

    public async Task SetItemAsync(string key, T item)
    {
        Valid.ValidStringSemLength(key);
        var valueJson = JsonSerializer.Serialize<T>(item);
        await _distributedCache.SetStringAsync(key, valueJson, _options);
    }

    public async Task SetListItemAsync(string key, List<T> itens)
    {
        Valid.ValidStringSemLength(key);
        var valuesJson = JsonSerializer.Serialize<List<T>>(itens);
        await _distributedCache.SetStringAsync(key, valuesJson, _options);
    }
}
