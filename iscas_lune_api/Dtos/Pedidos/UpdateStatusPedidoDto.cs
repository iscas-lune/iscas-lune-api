using iscas_lune_api.Domain.Entities;

namespace iscas_lune_api.Dtos.Pedidos;

public class UpdateStatusPedidoDto
{
    public Guid PedidoId { get; set; }
    public StatusPedido StatusPedido { get; set; }
}
