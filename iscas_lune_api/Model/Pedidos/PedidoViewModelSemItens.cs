using iscas_lune_api.Domain.Entities;

namespace iscas_lune_api.Model.Pedidos;

public class PedidoViewModelSemItens
{
    public Guid Id { get; set; }
    public DateTime DataCriacao { get; set; }
    public long Numero { get; set; }
    public StatusPedido StatusPedido { get; set; }
    public decimal ValorTotal { get; set; }
}
