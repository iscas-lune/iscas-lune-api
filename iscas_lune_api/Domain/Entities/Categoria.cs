using iscaslune.Api.Domain.Entities.Bases;
using System.Text.Json.Serialization;

namespace iscaslune.Api.Domain.Entities;

public sealed class Categoria : BaseEntity
{
    [JsonConstructor]
    public Categoria(Guid id, DateTime dataCriacao, DateTime dataAtualizacao, long numero, string descricao, byte[]? foto) : base(id, dataCriacao, dataAtualizacao, numero)
    {
        Descricao = descricao;
        Foto = foto;
    }

    public string Descricao { get; private set; }
    public byte[]? Foto { get; private set; }
    public List<Produto> Produtos { get; set; }
}
