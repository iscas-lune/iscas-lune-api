using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Domain.Entities.Bases;

namespace iscas_lune_api.Domain.Entities;

public sealed class PrecoProduto : BaseEntity
{
    public PrecoProduto(Guid id, DateTime dataCriacao, DateTime dataAtualizacao, long numero, decimal preco, decimal? precoPromocional, decimal? precoCusto, Guid tamanhoId)
        : base(id, dataCriacao, dataAtualizacao, numero)
    {
        Preco = preco;
        PrecoPromocional = precoPromocional;
        PrecoCusto = precoCusto;
        TamanhoId = tamanhoId;
    }

    public decimal Preco { get; private set; }
    public decimal? PrecoPromocional { get; private set; }
    public decimal? PrecoCusto { get; private set; }
    public Guid TamanhoId { get; private set; }
    public Tamanho Tamanho { get; set; } = null!;
}
