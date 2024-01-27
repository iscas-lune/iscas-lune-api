using iscas_lune_api.Domain.Entities;
using iscas_lune_api.Dtos.Pedidos;

namespace iscas_lune_api.Application.Interfaces;

public interface IProcessarPedidoService
{
    Task<Pedido> ProcessarAsync(Pedido pedido, PedidoCreateDto pedidoCreateDto);
}
