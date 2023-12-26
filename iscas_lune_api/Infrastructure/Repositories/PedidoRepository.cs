using iscas_lune_api.Domain.Entities;
using iscas_lune_api.Infrastructure.Interfaces;
using iscaslune.Api.Domain.Context;
using iscaslune.Api.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace iscas_lune_api.Infrastructure.Repositories;

public class PedidoRepository : GenericRepository<Pedido>, IPedidoRepository
{
    private readonly IscasLuneContext _context;
    public PedidoRepository(IscasLuneContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Pedido?> GetPedidoByIdAsync(Guid id)
    {
        return await _context
            .Pedidos
            .Include(x => x.ItensPedido)
                .ThenInclude(x => x.Produto)
            .Include(x => x.Usuario)
            .Include(x => x.ItensPedido)
                .ThenInclude(x => x.Tamanho)
            .Include(x => x.ItensPedido)
                .ThenInclude(x => x.Peso)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Pedido>?> GetPedidosByUsuarioIdAsync(Guid usuarioId, int statusPedido)
    {
        return await _context.Pedidos
            .AsQueryable()
            .Include(x => x.ItensPedido)
                .ThenInclude(x => x.Produto)
            .Include(x => x.ItensPedido)
                .ThenInclude(x => x.Tamanho)
            .Include(x => x.ItensPedido)
                .ThenInclude(x => x.Peso)
            .Where(x => x.UsuarioId == usuarioId && x.StatusPedido == (StatusPedido)statusPedido)
            .ToListAsync();
    }
}
