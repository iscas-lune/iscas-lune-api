using iscaslune.Api.Domain.Entities.Bases;
using System.Text.Json.Serialization;

namespace iscaslune.Api.Domain.Entities;

[method: JsonConstructor]
public sealed class Categoria(Guid id, DateTime dataCriacao, DateTime dataAtualizacao, long numero, string descricao, byte[]? foto)
    : BaseEntity(id, dataCriacao, dataAtualizacao, numero)
{
    public string Descricao { get; private set; } = descricao;
    public byte[]? Foto { get; private set; } = foto;
    public List<Produto> Produtos { get; set; } = new();
}
