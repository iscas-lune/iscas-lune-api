using iscas_lune_api.Application.Interfaces;
using iscas_lune_api.Domain.Entities;
using iscas_lune_api.Dtos.Pedidos;
using iscas_lune_api.Exceptions;
using iscas_lune_api.Infrastructure.Interfaces;
using iscas_lune_api.Model.Carrinho;
using iscaslune.Api.Application.Interfaces;
using iscaslune.Api.Migrations;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace iscas_lune_api.Application.Services;

public class ProcessarPedidoService : IProcessarPedidoService
{
    private readonly IPedidosEmAbertoRepository _pedidosEmAbertoRepository;
    private readonly ICachedService<CarrinhoModel> _cachedService;
    private readonly ITabelaDePrecoRepository _tabelaDePrecoRepository;

    public ProcessarPedidoService(
        ICachedService<CarrinhoModel> cachedService,
        ITabelaDePrecoRepository tabelaDePrecoRepository,
        IPedidosEmAbertoRepository pedidosEmAbertoRepository)
    {
        _cachedService = cachedService;
        _tabelaDePrecoRepository = tabelaDePrecoRepository;
        _pedidosEmAbertoRepository = pedidosEmAbertoRepository;
    }

    public async Task<Pedido> ProcessarAsync(Pedido pedido, PedidoCreateDto pedidoCreateDto)
    {
        var tabelaDePreco = await _tabelaDePrecoRepository.GetTabelaDePrecoAtivaEcommerceAsync()
            ?? throw new ExceptionApi("Tabela de preço não localizada!");

        var pedidosPorPeso = MontarListaPesos(pedidoCreateDto.PedidosPorPeso, pedido.Id, tabelaDePreco);
        var pedidosPorTamanho = MontarListaTamanhos(pedidoCreateDto.PedidosPorTamanho, pedido.Id, tabelaDePreco);

        pedido.ItensPedido.AddRange(pedidosPorPeso);
        pedido.ItensPedido.AddRange(pedidosPorTamanho);

        await _cachedService.RemoveCachedAsync($"carrinho-{pedido.UsuarioId}");
        var pedidoEmAberto = new PedidosEmAberto()
        {
            Id = Guid.NewGuid(),
            PedidoId = pedido.Id,
        };

        await _pedidosEmAbertoRepository.AddAsync(pedidoEmAberto);

        return pedido;
    }

    private static List<ItensPedido> MontarListaTamanhos(List<PedidoPorTamanhoCreateDto> pedidosPorTamanho, Guid pedidoId, TabelaDePreco tabelaDePreco)
    {
        var date = DateTime.Now;
        return pedidosPorTamanho.Select(x =>
        {
            var valorUnitario = tabelaDePreco
                .ItensTabelaDePreco
                .FirstOrDefault(item => item.ProdutoId == x.ProdutoId && item.TamanhoId == x.TamanhoId)?.ValorUnitario ?? 0;
            return new ItensPedido(Guid.NewGuid(), date, date, 0, x.ProdutoId, pedidoId, valorUnitario, x.Quantidade, null, x.TamanhoId);
        }).ToList();
    }

    private static List<ItensPedido> MontarListaPesos(List<PedidoPorPesoCreateDto> pedidosPorPeso, Guid pedidoId, TabelaDePreco tabelaDePreco)
    {
        var date = DateTime.Now;
        return pedidosPorPeso.Select(x =>
        {
            var valorUnitario = tabelaDePreco
                .ItensTabelaDePreco
                .FirstOrDefault(item => item.ProdutoId == x.ProdutoId && item.PesoId == x.PesoId)?.ValorUnitario ?? 0;
            return new ItensPedido(Guid.NewGuid(), date, date, 0, x.ProdutoId, pedidoId, valorUnitario, x.Quantidade, x.PesoId, null);
        }).ToList();
    }
}
