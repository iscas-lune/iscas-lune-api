using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Domain.Entities.Bases;

namespace iscas_lune_api.Domain.Entities;

public sealed class PrecoProdutoPeso : BaseEntity
{
    public PrecoProdutoPeso(Guid id, DateTime dataCriacao, DateTime dataAtualizacao, long numero, decimal preco, decimal? precoPromocional, decimal? precoCusto, Guid pesoId)
        : base(id, dataCriacao, dataAtualizacao, numero)
    {
        Preco = preco;
        PrecoPromocional = precoPromocional;
        PrecoCusto = precoCusto;
        PesoId = pesoId;
    }

    public decimal Preco { get; private set; }
    public decimal? PrecoPromocional { get; private set; }
    public decimal? PrecoCusto { get; private set; }
    public Guid PesoId { get; private set; }
    public Peso Peso { get; set; } = null!;
}
