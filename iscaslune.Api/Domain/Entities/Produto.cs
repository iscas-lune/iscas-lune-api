using iscaslune.Api.Domain.Entities.Bases;
using System.Text.Json.Serialization;

namespace iscaslune.Api.Domain.Entities;

[method: JsonConstructor]
public sealed class Produto(Guid id, DateTime dataCriacao, DateTime dataAtualizacao, long numero, string descricao, string? especificacaoTecnica, byte[] foto, Guid categoriaId, string? referencia)
    : BaseEntity(id, dataCriacao, dataAtualizacao, numero)
{
    public string Descricao { get; private set; } = descricao;
    public string? EspecificacaoTecnica { get; private set; } = especificacaoTecnica;
    public byte[] Foto { get; private set; } = foto;
    public List<Tamanho> Tamanhos { get; set; } = new();
    public List<Cor> Cores { get; set; } = new();
    public Guid CategoriaId { get; private set; } = categoriaId;
    public Categoria Categoria { get; set; } = null!;
    public string? Referencia { get; private set; } = referencia;
}
