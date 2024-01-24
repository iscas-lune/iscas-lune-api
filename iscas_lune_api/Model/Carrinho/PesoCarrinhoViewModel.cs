using iscas_lune_api.Model.Base;

namespace iscas_lune_api.Model.Carrinho;

public class PesoCarrinhoViewModel : BaseViewModel
{
    public string Descricao { get; set; } = string.Empty;
    public QuantidadeProdutoCarrinhoViewModel PrecoProduto { get; set; } = new();
}
