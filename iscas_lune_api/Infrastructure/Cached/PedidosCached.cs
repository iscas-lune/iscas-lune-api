using iscas_lune_api.Domain.Entities;
using iscas_lune_api.Infrastructure.Interfaces;
using iscas_lune_api.Infrastructure.Repositories;
using iscaslune.Api.Application.Interfaces;
using iscaslune.Api.Domain.Context;
using iscaslune.Api.Infrastructure.Repositories;

namespace iscas_lune_api.Infrastructure.Cached;

public class PedidosCached : GenericRepository<Pedido>, IPedidoRepository
{
    private readonly ICachedService<Pedido> _cachedService;
    private readonly PedidoRepository _pedidoRepository;
    public PedidosCached(IscasLuneContext context, ICachedService<Pedido> cachedService, PedidoRepository pedidoRepository) : base(context)
    {
        _cachedService = cachedService;
        _pedidoRepository = pedidoRepository;
    }

    public async Task<List<Pedido>?> GetPedidosByUsuarioIdAsync(Guid usuarioId, int statusPedido)
    {
        var key = $"pedidos-{usuarioId}-{statusPedido}";
        var pedidos = await _cachedService.GetListItemAsync(key);

        if (pedidos == null)
        {
            pedidos = await _pedidoRepository.GetPedidosByUsuarioIdAsync(usuarioId, statusPedido);

            if (pedidos?.Count > 0)
            {
                pedidos.ForEach(pedido =>
                {
                    pedido.ItensPedido.ForEach(item =>
                    {
                        item.Pedido = null;
                        item.Produto.ItensPedido = new();
                    });
                });
                await _cachedService.SetListItemAsync(key, pedidos);
            }
        }

        return pedidos;
    }
}
