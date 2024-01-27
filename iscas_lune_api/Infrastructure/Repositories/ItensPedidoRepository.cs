using iscas_lune_api.Domain.Entities;
using iscas_lune_api.Infrastructure.Interfaces;
using iscaslune.Api.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace iscas_lune_api.Infrastructure.Repositories;

public class ItensPedidoRepository : IItensPedidoRepository
{
    private readonly IscasLuneContext _context;

    public ItensPedidoRepository(IscasLuneContext context)
    {
        _context = context;
    }

    public async Task<List<ItensPedido>> GetItensPedidoByPedidoIdAsync(Guid pedidoId)
    {
        var itens = await _context
            .ItensPedidos
            .AsNoTracking()
            .Include(x => x.Produto)
            .Include(x => x.Tamanho)
            .Include(x => x.Peso)
            .Where(x => x.PedidoId == pedidoId)
            .ToListAsync();

        foreach (var item in itens)
        {
            item.Produto.ItensPedido = new();

            if(item.Tamanho != null)
                item.Tamanho.ItensPedido = new();

            if (item.Peso != null)
                item.Peso.ItensPedido = new();

            item.Pedido = null;
        }

        return itens;
    }
}
