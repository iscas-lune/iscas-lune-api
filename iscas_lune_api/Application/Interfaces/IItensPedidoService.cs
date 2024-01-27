using iscas_lune_api.Model.Pedidos;

namespace iscas_lune_api.Application.Interfaces;

public interface IItensPedidoService
{
    Task<List<ItensPedidoViewModel>> GetItensByPedidoIdAsync(Guid predidoId);
}
