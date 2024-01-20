using iscas_lune_api.Application.Interfaces;
using iscas_lune_api.Domain.Entities;
using iscas_lune_api.Dtos.Pedidos;
using iscas_lune_api.Infrastructure.Interfaces;
using iscas_lune_api.Model.Carrinho;
using iscas_lune_api.Model.Pedidos;
using iscaslune.Api.Application.Interfaces;

namespace iscas_lune_api.Application.Services;

public class PedidoService : IPedidoService
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IPedidosEmAbertoRepository _pedidosEmAbertoRepository;
    private readonly ITokenService _tokenService;
    private readonly ICachedService<CarrinhoModel> _cachedService;

    public PedidoService(IPedidoRepository pedidoRepository, ITokenService tokenService, ICachedService<CarrinhoModel> cachedService, IPedidosEmAbertoRepository pedidosEmAbertoRepository)
    {
        _pedidoRepository = pedidoRepository;
        _tokenService = tokenService;
        _cachedService = cachedService;
        _pedidosEmAbertoRepository = pedidosEmAbertoRepository;
    }

    public async Task<(string? error, bool result)> CreatePedidoAsync(PedidoCreateDto pedidoCreateDto)
    {
        var claims = _tokenService.GetClaims();
        var date = DateTime.Now;
        var pedido = new Pedido(Guid.NewGuid(), date, date, 0, StatusPedido.Aberto, claims.Id);

        var pedidosPorPeso = pedidoCreateDto.PedidosPorPeso.Select(x =>
        {
            return new ItensPedido(Guid.NewGuid(), date, date, 0, x.ProdutoId, pedido.Id, x.ValorUnitario, x.Quantidade, x.PesoId, null);
        }).ToList();

        var pedidosPorTamanho = pedidoCreateDto.PedidosPorTamanho.Select(x =>
        {
            return new ItensPedido(Guid.NewGuid(), date, date, 0, x.ProdutoId, pedido.Id, x.ValorUnitario, x.Quantidade, null, x.TamanhoId);
        }).ToList();

        pedido.ItensPedido.AddRange(pedidosPorPeso);
        pedido.ItensPedido.AddRange(pedidosPorTamanho);

        var result = await _pedidoRepository.AddAsync(pedido);

        if (!result) return ("Ocorreu um erro interno, tente novamente mais tarde!", false);
        await _cachedService.RemoveCachedAsync($"carrinho-{claims.Id}");
        var pedidoEmAberto = new PedidosEmAberto() 
        {
            Id = Guid.NewGuid(),
            PedidoId = pedido.Id,
        };

        await _pedidosEmAbertoRepository.AddAsync(pedidoEmAberto);

        return (null, true);
    }

    public async Task<List<PedidoViewModel>> GetPedidosUsuario(int statusPedido)
    {
        var claims = _tokenService.GetClaims();
        var pedidos = await _pedidoRepository.GetPedidosByUsuarioIdAsync(claims.Id, statusPedido);

        return pedidos?.Select(x => new PedidoViewModel().ForModel(x) ?? new()).ToList() ?? new();
    }

    public async Task<bool> UpdateStatusPedidoAsync(UpdateStatusPedidoDto updateStatusPedidoDto)
    {
        var pedido = await _pedidoRepository.GetPedidoByUpdateStatusAsync(updateStatusPedidoDto.PedidoId);

        if(pedido == null) return false;

        pedido.UpdateStatus(updateStatusPedidoDto.StatusPedido);

        return await _pedidoRepository.UpdateAsync(pedido);
    }
}
