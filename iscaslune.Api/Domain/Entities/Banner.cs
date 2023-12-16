using iscaslune.Api.Domain.Entities.Bases;
using System.Text.Json.Serialization;

namespace iscaslune.Api.Domain.Entities;

[method: JsonConstructor]
public sealed class Banner(Guid id, DateTime dataCriacao, DateTime dataAtualizacao,long numero, byte[] foto, bool ativo) 
    : BaseEntity(id, dataCriacao, dataAtualizacao, numero)
{
    public byte[] Foto { get; private set; } = foto;
    public bool Ativo { get; private set; } = ativo;
}
