using iscaslune.Api.Domain.Context;
using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Dtos.Cores;
using iscaslune.Api.Infrastructure.Filtros.Filtros;
using iscaslune.Api.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace iscaslune.Api.Infrastructure.Repositories;

public class CorRepository : GenericRepository<Peso>, ICorRepository
{
    private readonly IscasLuneContext _context;
    public CorRepository(IscasLuneContext context) : base(context)
    {
        _context = context;
    }
    public async Task<Peso?> GetCorByIdAsync(Guid id)
    {
        return await _context
            .Pesos
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Peso>> GetCoresAsync(PaginacaoPesoDto filterModel)
    {
        return await _context
            .Pesos
            .AsNoTracking()
            .AsQueryable()
            .FilterAll(filterModel)
            .ToListAsync();
    }
}
