using System.Text.Json.Serialization;

namespace iscas_lune_api.Model.Carrinho;

public class QuantidadeProdutoCarrinhoViewModel
{
    public Guid Id { get; set; }
    public decimal Quantidade { get; set; }
}
