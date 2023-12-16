using iscaslune.Api.Domain.Entities.Bases;
using System.Text.Json.Serialization;

namespace iscaslune.Api.Domain.Entities;

public sealed class Banner : BaseEntity
{
    [JsonConstructor]
    public Banner(Guid id, DateTime dataCriacao, DateTime dataAtualizacao, long numero, byte[] foto, bool ativo) : base(id, dataCriacao, dataAtualizacao, numero)
    {
        Foto = foto;
        Ativo = ativo;
    }

    public byte[] Foto { get; private set; }
    public bool Ativo { get; private set; }
}
