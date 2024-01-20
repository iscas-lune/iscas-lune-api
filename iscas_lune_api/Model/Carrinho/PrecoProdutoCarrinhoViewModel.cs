using System.Text.Json.Serialization;

namespace iscas_lune_api.Model.Carrinho;

public class PrecoProdutoCarrinhoViewModel
{
    public Guid Id { get; set; }
    public decimal Preco { get; set; }
    public decimal? PrecoPromocional { get; set; }
    public decimal? PrecoCusto { get; set; }
    public decimal Quantidade { get; set; }
}
