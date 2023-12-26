using iscaslune.Api.Domain.Context;
using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Dtos.Produtos;
using iscaslune.Api.Infrastructure.Filtros.Filtros;
using iscaslune.Api.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace iscaslune.Api.Infrastructure.Repositories;

public class ProdutoRepository
    : GenericRepository<Produto>, IProdutoRepository
{
    private readonly IscasLuneContext _context;

    public ProdutoRepository(IscasLuneContext context) : base(context)
    {
        _context = context;    
    }

    public async Task<Produto?> GetProdutoByIdAsync(Guid id)
    {
        return await _context
            .Produtos
            .Include(x => x.Categoria)
            .Include(x => x.Pesos)
                .ThenInclude(x => x.PrecoProdutoPeso)
            .Include(x => x.Tamanhos)
                .ThenInclude(x => x.PrecoProduto)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Produto>?> GetProdutosAsync(PaginacaoProdutoDto paginacaoProduto)
    {
        return await _context
            .Produtos
            .AsQueryable()
            .Include(x => x.Categoria)
            .Include(x => x.Pesos)
                .ThenInclude(x => x.PrecoProdutoPeso)
            .Include(x => x.Tamanhos)
                .ThenInclude(x => x.PrecoProduto)
            .FilterAll(paginacaoProduto)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Produto>> GetProdutosByCarrinhoAsync(List<Guid> produtosIds)
    {
        var produtos = await _context
            .Produtos
            .AsQueryable()
            .Include(x => x.Categoria)
            .Include(x => x.Pesos)
                .ThenInclude(x => x.PrecoProdutoPeso)
            .Include(x => x.Tamanhos)
                .ThenInclude(x => x.PrecoProduto)
            .Where(x => produtosIds.Contains(x.Id))
            .AsNoTracking()
            .ToListAsync();

        produtos.ForEach(produto =>
        {
            produto.Categoria.Produtos = new();
            produto.Pesos.ForEach(peso =>
            {
                if (peso.PrecoProdutoPeso != null)
                    peso.PrecoProdutoPeso.Peso = null;
                peso.Produtos = new();
            });
            produto.Tamanhos.ForEach(tamanho =>
            {
                if(tamanho.PrecoProduto != null)
                    tamanho.PrecoProduto.Tamanho = null;
                tamanho.Produtos = new();
            });
        });

        return produtos;
    }

    public async Task<List<Produto>?> GetProdutosByCategoriaAsync(Guid categoriaId)
    {
        return await _context
            .Produtos
            .AsQueryable()
            .Include(x => x.Categoria)
            .Include(x => x.Pesos)
                .ThenInclude(x => x.PrecoProdutoPeso)
            .Include(x => x.Tamanhos)
                .ThenInclude(x => x.PrecoProduto)
            .Where(x => x.CategoriaId == categoriaId)
            .AsNoTracking()
            .ToListAsync();
    }
}
