using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Domain.Entities.Bases;

namespace iscas_lune_api.Domain.Entities;

public sealed class ItensPedido : BaseEntity
{
    public ItensPedido(Guid id, DateTime dataCriacao, DateTime dataAtualizacao, long numero, Guid produtoId, Guid pedidoId, decimal valorUnitario, decimal quantidade, Guid? pesoId, Guid? tamanhoId) : base(id, dataCriacao, dataAtualizacao, numero)
    {
        ProdutoId = produtoId;
        PedidoId = pedidoId;
        ValorUnitario = valorUnitario;
        Quantidade = quantidade;
        PesoId = pesoId;
        TamanhoId = tamanhoId;
    }

    public Guid? PesoId { get; private set; }
    public Peso? Peso { get; set; }
    public Guid? TamanhoId { get; private set; }
    public Tamanho? Tamanho { get; set; }
    public Guid ProdutoId { get; private set; }
    public Produto Produto { get; set; } = null!;
    public Guid PedidoId { get; private set; }
    public Pedido Pedido { get; set; } = null!;
    public decimal ValorUnitario { get; private set; }
    public decimal Quantidade { get; private set; }
    public decimal ValorTotal { get 
        {
            return ValorUnitario * Quantidade;
        } 
    }
}
