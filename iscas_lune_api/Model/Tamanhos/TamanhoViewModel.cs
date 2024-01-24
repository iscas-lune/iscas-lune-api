using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Model.Base;

namespace iscaslune.Api.Model.Tamanhos;

public class TamanhoViewModel : BaseModel<Tamanho, TamanhoViewModel>
{
    public string Descricao { get; set; } = string.Empty;
    public override TamanhoViewModel? ForModel(Tamanho? entity)
    {
        if (entity == null) return null;

        Id = entity.Id;
        DataCriacao = entity.DataCriacao;
        Numero = entity.Numero;
        Descricao = entity.Descricao;

        return this;
    }
}
