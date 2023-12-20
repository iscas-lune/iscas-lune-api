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

    public async Task<List<Pedido>?> GetPedidosByUsuarioIdAsync(Guid usuarioId)
    {
        return await _context.Pedidos
            .AsQueryable()
            .Include(x => x.ItensPedido)
                .ThenInclude(x => x.Produto)
            .Where(x => x.UsuarioId == usuarioId)
            .ToListAsync();
    }
}
