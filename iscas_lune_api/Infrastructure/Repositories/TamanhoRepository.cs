using iscaslune.Api.Domain.Context;
using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Dtos.Tamanhos;
using iscaslune.Api.Infrastructure.Filtros.Filtros;
using iscaslune.Api.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace iscaslune.Api.Infrastructure.Repositories;

public class TamanhoRepository
    : GenericRepository<Tamanho>, ITamanhoRepository
{
    private readonly IscasLuneContext _context;

    public TamanhoRepository(IscasLuneContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Tamanho?> GetTamanhoByIdAsync(Guid id)
    {
        return await _context.Tamanhos
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);  
    }

    public async Task<List<Tamanho>?> GetTamanhosAsync(PaginacaoTamanhoDto paginacaoTamanhoDto)
    {
        return await _context
            .Tamanhos
            .AsQueryable()
            .FilterAll(paginacaoTamanhoDto)
            .AsNoTracking()
            .ToListAsync();
    }
}
