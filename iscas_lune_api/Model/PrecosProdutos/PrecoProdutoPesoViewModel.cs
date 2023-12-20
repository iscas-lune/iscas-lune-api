using iscas_lune_api.Domain.Entities;
using iscaslune.Api.Model.Base;
using System.Text.Json.Serialization;

namespace iscas_lune_api.Model.PrecosProdutos;

public class PrecoProdutoPesoViewModel : BaseModel<PrecoProdutoPeso, PrecoProdutoPesoViewModel>
{
    public decimal Preco { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? PrecoPromocional { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? PrecoCusto { get; set; }
    public override PrecoProdutoPesoViewModel? ForModel(PrecoProdutoPeso? entity)
    {
        if (entity == null) return null;

        Id = entity.Id;
        DataCriacao = entity.DataCriacao;
        Numero = entity.Numero;
        Preco = entity.Preco;
        PrecoPromocional = entity.PrecoPromocional;
        PrecoCusto = entity.PrecoCusto;
        return this;
    }
}
