using iscaslune.Api.Application.Interfaces;
using iscaslune.Api.Domain.Context;
using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Dtos.Cores;
using iscaslune.Api.Infrastructure.Filters.Model;
using iscaslune.Api.Infrastructure.Interfaces;
using iscaslune.Api.Infrastructure.Repositories;

namespace iscaslune.Api.Infrastructure.Cached;

public class PesoCached
    : GenericRepository<Peso>, IPesoRepository
{
    private readonly ICachedService<Peso> _cachedService;
    private readonly PesoRepository _corRepository;
    private const string _keyList = "cores";

    public PesoCached(IscasLuneContext context, ICachedService<Peso> cachedService, PesoRepository corRepository) : base(context)
    {
        _cachedService = cachedService;
        _corRepository = corRepository;
    }

    public async Task<Peso?> GetPesoByIdAsync(Guid id)
    {
        var key = id.ToString();
        var cor = await _cachedService.GetItemAsync(key);

        if (cor == null)
        {
            cor = await _corRepository.GetPesoByIdAsync(id);
            if(cor != null) await _cachedService.SetItemAsync(key, cor);
        }

        return cor;
    }

    public async Task<List<Peso>> GetPesosAsync(PaginacaoPesoDto filterModel)
    {
        if(!string.IsNullOrWhiteSpace(filterModel.Descricao) 
            || !filterModel.OrderBy.Equals("DataCriacao") 
            || filterModel.Asc)
            return await _corRepository.GetPesosAsync(filterModel);

        var cores = await _cachedService.GetListItemAsync(_keyList);

        if(cores == null || cores.Count == 0)
        {
            cores = await _corRepository.GetPesosAsync(filterModel);
            if (cores.Count > 0) await _cachedService.SetListItemAsync(_keyList, cores);
        }

        return cores;
    }
}
