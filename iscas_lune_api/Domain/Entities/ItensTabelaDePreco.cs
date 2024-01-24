using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Domain.Entities.Bases;

namespace iscas_lune_api.Domain.Entities;

public class ItensTabelaDePreco : BaseEntity
{
    public ItensTabelaDePreco(Guid id, DateTime dataCriacao, DateTime dataAtualizacao, long numero, decimal valorUnitario, Guid produtoId, Guid tabelaDePrecoId, Guid? tamanhoId, Guid? pesoId)
        : base(id, dataCriacao, dataAtualizacao, numero)
    {
        ValorUnitario = valorUnitario;
        ProdutoId = produtoId;
        TabelaDePrecoId = tabelaDePrecoId;
        TamanhoId = tamanhoId;
        PesoId = pesoId;
    }

    public decimal ValorUnitario { get; private set; }
    public Guid ProdutoId { get; private set; }
    public Produto Produto { get; set; } = null!;
    public Guid TabelaDePrecoId { get; private set; }
    public TabelaDePreco TabelaDePreco { get; set; } = null!;
    public Guid? TamanhoId { get; private set; }
    public Guid? PesoId { get; private set; }
}
