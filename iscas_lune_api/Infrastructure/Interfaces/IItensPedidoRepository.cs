using iscas_lune_api.Domain.Entities;

namespace iscas_lune_api.Infrastructure.Interfaces;

public interface IItensPedidoRepository
{
    Task<List<ItensPedido>> GetItensPedidoByPedidoIdAsync(Guid pedidoId);
}
