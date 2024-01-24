using iscas_lune_api.Domain.Entities;
using iscas_lune_api.Infrastructure.Interfaces;
using iscaslune.Api.Domain.Context;
using iscaslune.Api.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace iscas_lune_api.Infrastructure.Repositories;

public class TabelaDePrecoRepository : GenericRepository<TabelaDePreco>, ITabelaDePrecoRepository
{
    private readonly IscasLuneContext _context;
    public TabelaDePrecoRepository(IscasLuneContext context) : base(context)
    {
        _context = context;
    }

    public async Task<TabelaDePreco?> GetTabelaDePrecoAtivaEcommerceAsync()
    {
        return await _context.TabelaDePreco
            .AsNoTracking()
            .Include(x => x.ItensTabelaDePreco)
            .FirstOrDefaultAsync(x => x.AtivaEcommerce);
    }
}
