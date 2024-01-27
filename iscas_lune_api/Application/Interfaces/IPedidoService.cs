using iscas_lune_api.Dtos.Pedidos;
using iscas_lune_api.Model.Paginacao;
using iscas_lune_api.Model.Pedidos;

namespace iscas_lune_api.Application.Interfaces;

public interface IPedidoService
{
    Task<(string? error, bool result)> CreatePedidoAsync(PedidoCreateDto pedidoCreateDto);
    Task<List<PedidoViewModelSemItens>> GetPedidosUsuarioAsync(int statusPedido);
    Task<bool> UpdateStatusPedidoAsync(UpdateStatusPedidoDto updateStatusPedidoDto);
    Task<PaginacaoViewModel<PedidoViewModel>> GetPaginacaoAsync(int page);
}
