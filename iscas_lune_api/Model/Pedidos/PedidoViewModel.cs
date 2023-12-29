using iscas_lune_api.Domain.Entities;
using iscaslune.Api.Model.Base;

namespace iscas_lune_api.Model.Pedidos;

public class PedidoViewModel : BaseModel<Pedido, PedidoViewModel>
{
    public StatusPedido StatusPedido { get; set; }
    public decimal ValorTotal { get; set; }
    public List<ItensPedidoViewModel> ItensPedido { get; set; } = new();
    public override PedidoViewModel? ForModel(Pedido? entity)
    {
        if(entity == null) return null;

        Id = entity.Id;
        DataCriacao = entity.DataCriacao;
        Numero = entity.Numero;
        ValorTotal = entity.ValorTotal;
        StatusPedido = entity.StatusPedido;
        ItensPedido = entity.ItensPedido.Select(x =>
        {
            return new ItensPedidoViewModel().ForModel(x) ?? new();
        }).OrderBy(x => x.Produto.Numero).ToList();    
        return this;
    }
}
