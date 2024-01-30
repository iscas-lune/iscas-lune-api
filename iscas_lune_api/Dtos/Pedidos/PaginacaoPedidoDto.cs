using iscaslune.Api.Infrastructure.Filters.Model;

namespace iscas_lune_api.Dtos.Pedidos;
 
public class PaginacaoPedidoDto : FilterModel
{
    public int? StatusPedido { get; set; }
}
