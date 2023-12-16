using iscaslune.Api.Application.Interfaces;
using iscaslune.Api.Domain.Context;
using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Dtos.Cores;
using iscaslune.Api.Infrastructure.Filters.Model;
using iscaslune.Api.Infrastructure.Interfaces;
using iscaslune.Api.Infrastructure.Repositories;

namespace iscaslune.Api.Infrastructure.Cached;

public class CorCached(IscasLuneContext context, ICachedService<Cor> cachedService, CorRepository corRepository)
    : GenericRepository<Cor>(context), ICorRepository
{
    private readonly ICachedService<Cor> _cachedService = cachedService;
    private readonly CorRepository _corRepository = corRepository;
    private const string _keyList = "cores";

    public async Task<Cor?> GetCorByIdAsync(Guid id)
    {
        var key = id.ToString();
        var cor = await _cachedService.GetItemAsync(key);

        if (cor == null)
        {
            cor = await _corRepository.GetCorByIdAsync(id);
            if(cor != null) await _cachedService.SetItemAsync(key, cor);
        }

        return cor;
    }

    public async Task<List<Cor>> GetCoresAsync(PaginacaoCorDto filterModel)
    {
        if(!string.IsNullOrWhiteSpace(filterModel.Descricao) 
            || !filterModel.OrderBy.Equals("DataCriacao") 
            || filterModel.Asc)
            return await _corRepository.GetCoresAsync(filterModel);

        var cores = await _cachedService.GetListItemAsync(_keyList);

        if(cores == null || cores.Count == 0)
        {
            cores = await _corRepository.GetCoresAsync(filterModel);
            if (cores.Count > 0) await _cachedService.SetListItemAsync(_keyList, cores);
        }

        return cores;
    }
}
