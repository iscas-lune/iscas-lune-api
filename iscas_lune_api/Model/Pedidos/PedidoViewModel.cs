using iscas_lune_api.Domain.Entities;
using iscaslune.Api.Model.Base;

namespace iscas_lune_api.Model.Pedidos;

public class PedidoViewModel : BaseModel<Pedido, PedidoViewModel>
{
    public StatusPedido StatusPedido { get; set; }
    public decimal ValorTotal { get; set; }
    public string Usuario { get; set; } = string.Empty;
    public List<ItensPedidoViewModel> ItensPedido { get; set; } = new();
    public override PedidoViewModel? ForModel(Pedido? entity)
    {
        if (entity == null) return null;

        Id = entity.Id;
        DataCriacao = entity.DataCriacao;
        Numero = entity.Numero;
        ValorTotal = entity.ValorTotal;
        StatusPedido = entity.StatusPedido;
        Usuario = entity.Usuario.Nome;

        return this;
    }
}
