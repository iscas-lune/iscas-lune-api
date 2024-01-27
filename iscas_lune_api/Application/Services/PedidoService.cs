using iscas_lune_api.Application.Interfaces;
using iscas_lune_api.Domain.Entities;
using iscas_lune_api.Dtos.Pedidos;
using iscas_lune_api.Exceptions;
using iscas_lune_api.Infrastructure.Interfaces;
using iscas_lune_api.Model.Carrinho;
using iscas_lune_api.Model.Paginacao;
using iscas_lune_api.Model.Pedidos;
using iscaslune.Api.Application.Interfaces;

namespace iscas_lune_api.Application.Services;

public class PedidoService : IPedidoService
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly ITokenService _tokenService;
    private readonly IProcessarPedidoService _processarPedidoService;

    public PedidoService(
        IPedidoRepository pedidoRepository,
        ITokenService tokenService,
        IProcessarPedidoService processarPedidoService)
    {
        _pedidoRepository = pedidoRepository;
        _tokenService = tokenService;
        _processarPedidoService = processarPedidoService;
    }

    public async Task<(string? error, bool result)> CreatePedidoAsync(PedidoCreateDto pedidoCreateDto)
    {
        if (pedidoCreateDto.PedidosPorPeso.Count == 0 && pedidoCreateDto.PedidosPorTamanho.Count == 0)
            throw new ExceptionApi("Informe os produtos");

        var claims = _tokenService.GetClaims();
        var date = DateTime.Now;
        var pedido = new Pedido(Guid.NewGuid(), date, date, 0, StatusPedido.Aberto, claims.Id);

        pedido = await _processarPedidoService.ProcessarAsync(pedido, pedidoCreateDto);

        var result = await _pedidoRepository.AddAsync(pedido);

        if (!result) return ("Ocorreu um erro interno, tente novamente mais tarde!", false);

        return (null, true);
    }

    public async Task<PaginacaoViewModel<PedidoViewModel>> GetPaginacaoAsync(int page)
    {
        var paginacao = await _pedidoRepository.GetPaginacaoPedidoAsync(page);

        return new()
        {
            TotalPage = paginacao.TotalPage,
            Values = paginacao.Values
                .Select(x => new PedidoViewModel().ForModel(x) ?? new())
                .ToList()
        };
    }

    public async Task<List<PedidoViewModelSemItens>> GetPedidosUsuarioAsync(int statusPedido)
    {
        var claims = _tokenService.GetClaims();
        return await _pedidoRepository.GetPedidosByUsuarioIdAsync(claims.Id, statusPedido);
    }

    public async Task<bool> UpdateStatusPedidoAsync(UpdateStatusPedidoDto updateStatusPedidoDto)
    {
        var pedido = await _pedidoRepository.GetPedidoByUpdateStatusAsync(updateStatusPedidoDto.PedidoId);

        if (pedido == null) return false;

        pedido.UpdateStatus(updateStatusPedidoDto.StatusPedido);

        return await _pedidoRepository.UpdateAsync(pedido);
    }
}
