namespace iscas_lune_api.Dtos.Pedidos;

public class PedidoPorPesoCreateDto
{
    public Guid ProdutoId { get; set; }
    public Guid PesoId { get; set; }
    public decimal Quantidade { get; set; }
}
