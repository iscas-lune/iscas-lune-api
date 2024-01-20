using iscas_lune_api.Infrastructure.Interfaces;
using iscaslune.Api.Domain.Context;
using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace iscas_lune_api.Infrastructure.Repositories;

public class TamanhoProdutoRepository : GenericRepository<TamanhosProdutos>, ITamanhoProdutoRepository
{
    private readonly IscasLuneContext _context;
    public TamanhoProdutoRepository(IscasLuneContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<TamanhosProdutos>?> GetTamanhosProdutosAsync()
    {
        return await _context
            .TamanhosProdutos
            .AsNoTracking()
            .Include(x => x.Tamanho)
            .ToListAsync(); 
    }
}
