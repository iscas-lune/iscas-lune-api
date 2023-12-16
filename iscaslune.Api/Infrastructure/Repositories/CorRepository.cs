using iscaslune.Api.Domain.Context;
using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Dtos.Cores;
using iscaslune.Api.Infrastructure.Filtros.Filtros;
using iscaslune.Api.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace iscaslune.Api.Infrastructure.Repositories;

public class CorRepository(IscasLuneContext context) : GenericRepository<Cor>(context), ICorRepository
{
    private readonly IscasLuneContext _context = context;
    public async Task<Cor?> GetCorByIdAsync(Guid id)
    {
        return await _context
            .Cores
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Cor>> GetCoresAsync(PaginacaoCorDto filterModel)
    {
        return await _context
            .Cores
            .AsQueryable()
            .FilterAll(filterModel)
            .ToListAsync();
    }
}
