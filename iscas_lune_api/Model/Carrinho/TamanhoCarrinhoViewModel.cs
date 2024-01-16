using iscas_lune_api.Model.Base;

namespace iscas_lune_api.Model.Carrinho;

public class TamanhoCarrinhoViewModel : BaseViewModel
{
    public string Descricao { get; set; } = string.Empty;
    public PrecoProdutoCarrinhoViewModel PrecoProduto { get; set; } = new();
}
