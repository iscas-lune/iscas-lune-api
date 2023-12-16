using iscaslune.Api.Domain.Context;
using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Dtos.Produtos;
using iscaslune.Api.Infrastructure.Filtros.Filtros;
using iscaslune.Api.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace iscaslune.Api.Infrastructure.Repositories;

public class ProdutoRepository(IscasLuneContext context) 
    : GenericRepository<Produto>(context), IProdutoRepository
{
    private readonly IscasLuneContext _context = context;

    public async Task<Produto?> GetProdutoByIdAsync(Guid id)
    {
        return await _context
            .Produtos
            .Include(x => x.Categoria)
            .Include(x => x.Cores)
            .Include(x => x.Tamanhos)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Produto>?> GetProdutosAsync(PaginacaoProdutoDto paginacaoProduto)
    {
        return await _context
            .Produtos
            .AsQueryable()
            .FilterAll(paginacaoProduto)
            .ToListAsync();
    }
}
