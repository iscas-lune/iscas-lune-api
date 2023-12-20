namespace iscas_lune_api.Dtos.Pedidos;

public class PedidoCreateDto
{
    public List<PedidoPorPesoCreateDto> PedidosPorPeso { get; set; } = new();
    public List<PedidoPorTamanhoCreateDto> PedidosPorTamanho { get; set; } = new();
}
