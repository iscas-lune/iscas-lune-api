using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Model.Base;

namespace iscaslune.Api.Model.Cores;

public class PesoViewModel : BaseModel<Peso, PesoViewModel>
{
    public string Descricao { get; set; } = string.Empty;

    public override PesoViewModel? ForModel(Peso? entity)
    {
        if (entity == null) return null;

        Id = entity.Id;
        Descricao = entity.Descricao;
        DataCriacao = entity.DataCriacao;
        Numero = entity.Numero;
        return this;
    }
}
