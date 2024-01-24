using iscaslune.Api.Domain.Entities.Bases;

namespace iscas_lune_api.Domain.Entities;

public sealed class TabelaDePreco : BaseEntity
{
    public TabelaDePreco(Guid id, DateTime dataCriacao, DateTime dataAtualizacao, long numero, string descricao, bool ativaEcommerce)
        : base(id, dataCriacao, dataAtualizacao, numero)
    {
        Descricao = descricao;
        AtivaEcommerce = ativaEcommerce;
    }

    public string Descricao { get; private set; }
    public bool AtivaEcommerce { get; private set; }
    public List<ItensTabelaDePreco> ItensTabelaDePreco { get; set; } = new();

}
