using iscas_lune_api.Domain.Entities;
using iscaslune.Api.Infrastructure.Interfaces;

namespace iscas_lune_api.Infrastructure.Interfaces;

public interface IPedidoRepository : IGenericRepository<Pedido>
{
    Task<List<Pedido>?> GetPedidosByUsuarioIdAsync(Guid usuarioId, int statusPedido);
    Task<Pedido?> GetPedidoByIdAsync(Guid id);
}
