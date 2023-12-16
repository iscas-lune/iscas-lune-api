namespace iscaslune.Api.Domain.Entities.Bases;

public abstract class BaseEntity
{
    protected BaseEntity(Guid id, DateTime dataCriacao, DateTime dataAtualizacao, long numero)
    {
        Id = id;
        DataCriacao = dataCriacao;
        DataAtualizacao = dataAtualizacao;
        Numero = numero;
    }

    public Guid Id { get; protected set; }
    public DateTime DataCriacao { get; protected set; }
    public DateTime DataAtualizacao { get; protected set; }
    public long Numero { get; protected set; }
}
