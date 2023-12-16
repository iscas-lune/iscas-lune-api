using iscaslune.Api.Domain.Context;
using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Dtos.Categorias;
using iscaslune.Api.Infrastructure.Filtros.Filtros;
using iscaslune.Api.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace iscaslune.Api.Infrastructure.Repositories;

public class CategoriaRepository(IscasLuneContext context) 
    : GenericRepository<Categoria>(context), ICategoriaRepository
{
    private readonly IscasLuneContext _context = context;

    public async Task<Categoria?> GetCategoriaByIdAsync(Guid id)
    {
        return await _context.Categorias
            .Include(x => x.Produtos)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Categoria>?> GetCategoriasAsync(PaginacaoCategoriaDto paginacaoCategoriaDto)
    {
        return await _context
            .Categorias
            .AsQueryable()
            .Include(x => x.Produtos)
            .FilterAll(paginacaoCategoriaDto)
            .ToListAsync();
    }
}
