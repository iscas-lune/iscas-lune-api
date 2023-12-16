using iscaslune.Api.Domain.Entities.Bases;

namespace iscaslune.Api.Domain.Entities;

public sealed class Cor(Guid id, DateTime dataCriacao, DateTime dataAtualizacao, long numero, string descricao)
    : BaseEntity(id, dataCriacao, dataAtualizacao, numero)
{
    public string Descricao { get; private set; } = descricao;
    public List<Produto> Produtos { get; set; } = new();
}
