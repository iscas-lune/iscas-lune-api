using iscas_lune_api.Domain.Entities;
using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Model.Base;
using iscaslune.Api.Model.Cores;
using iscaslune.Api.Model.Produtos;
using iscaslune.Api.Model.Tamanhos;

namespace iscas_lune_api.Model.Pedidos;

public class ItensPedidoViewModel : BaseModel<ItensPedido, ItensPedidoViewModel>
{
    public Guid? PesoId { get; set; }
    public PesoViewModel? Peso { get; set; }
    public Guid? TamanhoId { get; set; }
    public TamanhoViewModel? Tamanho { get; set; }
    public Guid ProdutoId { get; set; }
    public ProdutoViewModel Produto { get; set; } = null!;
    public Guid PedidoId { get; set; }
    public decimal ValorUnitario { get; set; }
    public decimal Quantidade { get; set; }
    public decimal ValorTotal { get; set; }
    public override ItensPedidoViewModel? ForModel(ItensPedido? entity)
    {
        if (entity == null) return null;

        Id = entity.Id;
        DataCriacao = entity.DataCriacao;
        Numero = entity.Numero;
        PesoId = entity.PesoId;
        TamanhoId = entity.TamanhoId;
        ProdutoId = entity.ProdutoId;
        PedidoId = entity.PedidoId;
        Peso = new PesoViewModel().ForModel(entity.Peso);
        Tamanho = new TamanhoViewModel().ForModel(entity.Tamanho);
        Produto = new ProdutoViewModel().ForModel(entity.Produto) ?? new();
        ValorUnitario = entity.ValorUnitario;
        ValorTotal = entity.ValorTotal;
        Quantidade = entity.Quantidade;

        return this;
    }
}
