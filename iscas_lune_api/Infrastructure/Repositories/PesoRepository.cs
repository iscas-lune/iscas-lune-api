using iscaslune.Api.Domain.Context;
using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Dtos.Cores;
using iscaslune.Api.Infrastructure.Filtros.Filtros;
using iscaslune.Api.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace iscaslune.Api.Infrastructure.Repositories;

public class PesoRepository : GenericRepository<Peso>, IPesoRepository
{
    private readonly IscasLuneContext _context;
    public PesoRepository(IscasLuneContext context) : base(context)
    {
        _context = context;
    }
    public async Task<Peso?> GetPesoByIdAsync(Guid id)
    {
        return await _context
            .Pesos
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Peso>> GetPesosAsync(PaginacaoPesoDto filterModel)
    {
        return await _context
            .Pesos
            .AsNoTracking()
            .AsQueryable()
            .ToListAsync();
    }
}
