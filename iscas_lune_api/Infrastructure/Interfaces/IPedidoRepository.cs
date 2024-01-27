using iscas_lune_api.Domain.Entities;
using iscas_lune_api.Model.Paginacao;
using iscas_lune_api.Model.Pedidos;
using iscaslune.Api.Infrastructure.Interfaces;

namespace iscas_lune_api.Infrastructure.Interfaces;

public interface IPedidoRepository : IGenericRepository<Pedido>
{
    Task<List<PedidoViewModelSemItens>> GetPedidosByUsuarioIdAsync(Guid usuarioId, int statusPedido);
    Task<Pedido?> GetPedidoByIdAsync(Guid id);
    Task<Pedido?> GetPedidoByUpdateStatusAsync(Guid id);
    Task<PaginacaoViewModel<Pedido>> GetPaginacaoPedidoAsync(int page);
}
