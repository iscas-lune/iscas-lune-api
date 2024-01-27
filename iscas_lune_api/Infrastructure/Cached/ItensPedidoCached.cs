using iscas_lune_api.Domain.Entities;
using iscas_lune_api.Infrastructure.Interfaces;
using iscas_lune_api.Infrastructure.Repositories;
using iscaslune.Api.Application.Interfaces;

namespace iscas_lune_api.Infrastructure.Cached;

public class ItensPedidoCached : IItensPedidoRepository
{
    private readonly ItensPedidoRepository _itensPedidoRepository;
    private readonly ICachedService<ItensPedido> _cachedService;

    public ItensPedidoCached(ItensPedidoRepository itensPedidoRepository, ICachedService<ItensPedido> cachedService)
    {
        _itensPedidoRepository = itensPedidoRepository;
        _cachedService = cachedService;
    }

    public async Task<List<ItensPedido>> GetItensPedidoByPedidoIdAsync(Guid pedidoId)
    {
        var key = $"{pedidoId}";

        var itens = await _cachedService.GetListItemAsync(key);

        if (itens == null)
        {
            itens = await _itensPedidoRepository.GetItensPedidoByPedidoIdAsync(pedidoId);
            if (itens?.Count > 0)
                await _cachedService.SetListItemAsync(key, itens);
        }

        return itens ?? new();
    }
}
