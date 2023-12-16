using iscaslune.Api.Domain.Context;
using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Dtos.Tamanhos;
using iscaslune.Api.Infrastructure.Filtros.Filtros;
using iscaslune.Api.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace iscaslune.Api.Infrastructure.Repositories;

public class TamanhoRepository(IscasLuneContext context) 
    : GenericRepository<Tamanho>(context), ITamanhoRepository
{
    private readonly IscasLuneContext _context = context;

    public async Task<Tamanho?> GetTamanhoByIdAsync(Guid id)
    {
        return await _context.Tamanhos
            .FirstOrDefaultAsync(x => x.Id == id);  
    }

    public async Task<List<Tamanho>?> GetTamanhosAsync(PaginacaoTamanhoDto paginacaoTamanhoDto)
    {
        return await _context
            .Tamanhos
            .AsQueryable()
            .FilterAll(paginacaoTamanhoDto)
            .ToListAsync();
    }
}
