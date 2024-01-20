using iscas_lune_api.Domain.Entities;
using iscas_lune_api.Infrastructure.Interfaces;
using iscaslune.Api.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace iscas_lune_api.Infrastructure.Repositories;

public class PrecoProdutoRepository : IPrecoProdutoRepository
{
    private readonly IscasLuneContext _context;

    public PrecoProdutoRepository(IscasLuneContext context)
    {
        _context = context;
    }

    public async Task<List<PrecoProduto>> GetPrecosProdutosAsync()
    {
        return await _context
            .PrecosProduto
            .AsNoTracking()
            .ToListAsync();
    }
}
