using iscas_lune_api.Domain.Entities;
using iscaslune.Api.Domain.Entities.Bases;
using System.Text.Json.Serialization;

namespace iscaslune.Api.Domain.Entities;

public sealed class Produto : BaseEntity
{
    [JsonConstructor]
    public Produto(Guid id, DateTime dataCriacao, DateTime dataAtualizacao, long numero, string descricao, string? especificacaoTecnica, byte[] foto, Guid categoriaId, string? referencia) : base(id, dataCriacao, dataAtualizacao, numero)
    {
        Descricao = descricao;
        EspecificacaoTecnica = especificacaoTecnica;
        Foto = foto;
        CategoriaId = categoriaId;
        Referencia = referencia;
    }

    public string Descricao { get; private set; }
    public string? EspecificacaoTecnica { get; private set; } 
    public byte[] Foto { get; private set; }
    public List<Tamanho> Tamanhos { get; set; } = new();
    public List<Peso> Pesos { get; set; } = new();
    public Guid CategoriaId { get; private set; }
    public Categoria Categoria { get; set; } = null!;
    public List<ItensPedido> ItensPedido { get; set; } = new();
    public string? Referencia { get; private set; }
}
