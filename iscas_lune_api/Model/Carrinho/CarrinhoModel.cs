namespace iscas_lune_api.Model.Carrinho;

public class CarrinhoModel
{
    public Guid UsuarioId { get; set; }
    public List<Guid> ProdutosIds { get; set; } = new();
}
