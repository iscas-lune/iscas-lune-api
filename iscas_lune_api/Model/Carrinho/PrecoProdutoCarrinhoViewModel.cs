using System.Text.Json.Serialization;

namespace iscas_lune_api.Model.Carrinho;

public class PrecoProdutoCarrinhoViewModel
{
    public Guid Id { get; set; }
    public decimal Preco { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? PrecoPromocional { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? PrecoCusto { get; set; }
    public decimal Quantidade { get; set; }
}
