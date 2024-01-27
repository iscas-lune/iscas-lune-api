using iscas_lune_api.Application.Interfaces;
using iscas_lune_api.Infrastructure.Interfaces;
using iscas_lune_api.Model.Pedidos;

namespace iscas_lune_api.Application.Services;

public class ItensPedidoService : IItensPedidoService
{
    private readonly IItensPedidoRepository _itensPedidoRepository;

    public ItensPedidoService(IItensPedidoRepository itensPedidoRepository)
    {
        _itensPedidoRepository = itensPedidoRepository;
    }

    public async Task<List<ItensPedidoViewModel>> GetItensByPedidoIdAsync(Guid pedidoId)
    {
        var itens = await _itensPedidoRepository.GetItensPedidoByPedidoIdAsync(pedidoId);

        return itens
            .Select(item => 
                new ItensPedidoViewModel()
                    .ForModel(item) ?? new())
            .ToList();
    }
}
