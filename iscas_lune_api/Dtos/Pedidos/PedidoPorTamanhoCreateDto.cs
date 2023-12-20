namespace iscas_lune_api.Dtos.Pedidos;

public class PedidoPorTamanhoCreateDto
{
    public Guid ProdutoId { get; set; }
    public Guid TamanhoId { get; set; }
    public decimal Quantidade { get; set; }
    public decimal ValorUnitario { get; set; }
}
