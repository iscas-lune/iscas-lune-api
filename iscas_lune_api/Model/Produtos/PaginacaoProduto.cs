using iscaslune.Api.Model.Produtos;

namespace iscas_lune_api.Model.Produtos;

public class PaginacaoProduto
{
    public List<ProdutoViewModel> Produtos { get; set; } = new();
    public int TotalPage { get; set; }
}
