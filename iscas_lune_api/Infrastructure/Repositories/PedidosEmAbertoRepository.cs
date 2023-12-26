using iscas_lune_api.Domain.Entities;
using iscas_lune_api.Infrastructure.Interfaces;
using iscaslune.Api.Domain.Context;
using iscaslune.Api.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace iscas_lune_api.Infrastructure.Repositories;

public class PedidosEmAbertoRepository 
    : GenericRepository<PedidosEmAberto>, IPedidosEmAbertoRepository
{
    private readonly IscasLuneContext _context;
    public PedidosEmAbertoRepository(IscasLuneContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PedidosEmAberto?> GetFirstOrDefautlAsync()
    {
        return await _context.PedidosEmAberto.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
    }
}
