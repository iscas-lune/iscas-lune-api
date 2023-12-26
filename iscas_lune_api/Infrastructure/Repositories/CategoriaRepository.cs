using iscaslune.Api.Domain.Context;
using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Dtos.Categorias;
using iscaslune.Api.Infrastructure.Filtros.Filtros;
using iscaslune.Api.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace iscaslune.Api.Infrastructure.Repositories;

public class CategoriaRepository
    : GenericRepository<Categoria>, ICategoriaRepository
{
    private readonly IscasLuneContext _context;

    public CategoriaRepository(IscasLuneContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Categoria?> GetCategoriaByIdAsync(Guid id)
    {
        return await _context
            .Categorias
            .AsNoTracking()
            .Include(x => x.Produtos)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Categoria>?> GetCategoriasAsync(PaginacaoCategoriaDto paginacaoCategoriaDto)
    {
        var categorias = await _context
            .Categorias
            .AsQueryable()
            .AsNoTracking()
            .Include(x => x.Produtos)
            .FilterAll(paginacaoCategoriaDto)
            .ToListAsync();

        foreach (var categoria in categorias)
        {
            categoria.Produtos = categoria
                .Produtos
                .Take(3)
                .ToList();
        }

        return categorias;
    }
}
