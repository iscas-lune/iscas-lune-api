using iscas_lune_api.Domain.Entities;
using iscaslune.Api.Domain.Entities.Bases;

namespace iscaslune.Api.Domain.Entities;

public sealed class Peso : BaseEntity
{
    public Peso(Guid id, DateTime dataCriacao, DateTime dataAtualizacao, long numero, string descricao) : base(id, dataCriacao, dataAtualizacao, numero)
    {
        Descricao = descricao;
    }

    public string Descricao { get; private set; }
    public PrecoProdutoPeso PrecoProdutoPeso { get; set; } = null!;
    public List<ItensPedido> ItensPedido { get; set; } = new();
    public List<Produto> Produtos { get; set; } = new();
}
