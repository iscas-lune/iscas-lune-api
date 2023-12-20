using iscas_lune_api.Model.PrecosProdutos;
using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Model.Base;

namespace iscaslune.Api.Model.Cores;

public class PesoViewModel : BaseModel<Peso, PesoViewModel>
{
    public string Descricao { get; set; } = string.Empty;
    public PrecoProdutoPesoViewModel PrecoProduto { get; set; } = null!;

    public override PesoViewModel? ForModel(Peso? entity)
    {
        if (entity == null) return null;

        Id = entity.Id;
        Descricao = entity.Descricao;
        DataCriacao = entity.DataCriacao;
        Numero = entity.Numero;
        PrecoProduto = new PrecoProdutoPesoViewModel().ForModel(entity.PrecoProdutoPeso) ?? new();
        return this;
    }
}
