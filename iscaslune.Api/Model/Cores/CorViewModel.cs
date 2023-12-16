using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Model.Base;

namespace iscaslune.Api.Model.Cores;

public class CorViewModel : BaseModel<Cor, CorViewModel>
{
    public string Descricao { get; set; } = string.Empty;

    public override CorViewModel? ForModel(Cor? entity)
    {
        if (entity == null) return null;

        Id = entity.Id;
        Descricao = entity.Descricao;
        DataCriacao = entity.DataCriacao;
        Numero = entity.Numero;
        return this;
    }
}
