using iscas_lune_api.Model.PrecosProdutos;
using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Model.Base;

namespace iscaslune.Api.Model.Tamanhos;

public class TamanhoViewModel : BaseModel<Tamanho, TamanhoViewModel>
{
    public string Descricao { get; set; } = string.Empty;
    public PrecoProdutoModel PrecoProduto { get; set; } = null!;
    public override TamanhoViewModel? ForModel(Tamanho? entity)
    {
        if (entity == null) return null;

        Id = entity.Id;
        DataCriacao = entity.DataCriacao;
        Numero = entity.Numero;
        Descricao = entity.Descricao;
        PrecoProduto = new PrecoProdutoModel().ForModel(entity.PrecoProduto) ?? new();

        return this;
    }
}
