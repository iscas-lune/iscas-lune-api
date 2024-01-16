namespace iscas_lune_api.Dtos.Carrinhos;

public class AddCarrinhoDto
{
    public Guid ProdutoId { get; set; }
    public List<AddTamanhoCarrinho> Tamanhos { get; set; } = new();
    public List<AddPesoCarrinho> Pesos { get; set; } = new();
}
