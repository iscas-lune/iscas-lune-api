namespace iscaslune.Api.Model.Base;

public abstract class BaseModel<Entity, Model>
{
    public Guid Id { get; set; }
    public DateTime DataCriacao { get; set; }
    public long Numero { get; set; }

    public abstract Model? ForModel(Entity? entity);
}
