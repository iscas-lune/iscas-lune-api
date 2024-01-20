using iscas_lune_api.Infrastructure.Interfaces;
using iscaslune.Api.Domain.Context;
using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Dtos.Produtos;
using iscaslune.Api.Infrastructure.Filtros.Filtros;
using iscaslune.Api.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using System.Linq;

namespace iscaslune.Api.Infrastructure.Repositories;

public class ProdutoRepository
    : GenericRepository<Produto>, IProdutoRepository
{
    private readonly IscasLuneContext _context;
    private readonly ITamanhoProdutoRepository _tamanhoRepository;
    private readonly IPrecoProdutoRepository _precoProdutoRepository;

    public ProdutoRepository(IscasLuneContext context, ITamanhoProdutoRepository tamanhoRepository, IPrecoProdutoRepository precoProdutoRepository) : base(context)
    {
        _context = context;
        _tamanhoRepository = tamanhoRepository;
        _precoProdutoRepository = precoProdutoRepository;
    }

    public async Task<Produto?> GetProdutoByIdAsync(Guid id)
    {
        var produto = await _context
            .Produtos
            .Include(x => x.Categoria)
            .Include(x => x.Pesos)
                .ThenInclude(x => x.PrecoProdutoPeso)
            .Include(x => x.Tamanhos)
                .ThenInclude(x => x.PrecoProduto)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (produto != null)
        {
            produto.Categoria.Produtos = new();
            produto.Pesos.ForEach(peso =>
            {
                if (peso.PrecoProdutoPeso != null)
                    peso.PrecoProdutoPeso.Peso = null;
                peso.Produtos = new();
                peso.Produtos = new();
            });
            produto.Tamanhos.ForEach(tamanho =>
            {
                if (tamanho.PrecoProduto != null)
                    tamanho.PrecoProduto.Tamanho = null;
                tamanho.Produtos = new();
            });
        }

        return produto;
    }

    public async Task<List<Produto>?> GetProdutosAsync(PaginacaoProdutoDto paginacaoProduto)
    {
        var produtos = await _context
            .Produtos
            .AsNoTracking()
            .AsQueryable()
            .Include(x => x.Categoria)
            .ToListAsync();

        var tamanhos = await _tamanhoRepository.GetTamanhosProdutosAsync() ?? new();
        var precosProdutos = await _precoProdutoRepository.GetPrecosProdutosAsync();

        if (produtos.Count > 0)
        {
            produtos.ForEach(produto =>
            {
                produto.Categoria.Produtos = new();
                produto.Tamanhos = tamanhos
                    .Where(x => x.ProdutoId == produto.Id)
                    .Select(tm => 
                        new Tamanho(tm.Tamanho.Id, tm.Tamanho.DataCriacao, tm.Tamanho.DataAtualizacao, tm.Tamanho.Numero, tm.Tamanho.Descricao)
                        {
                            PrecoProduto = precosProdutos.FirstOrDefault(pr => pr.TamanhoId == tm.Tamanho.Id)
                        }
                     )
                    .ToList();
            });
        }

        return produtos;
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
                if (tamanho.PrecoProduto != null)
                    tamanho.PrecoProduto.Tamanho = null;
                tamanho.Produtos = new();
            });
        });

        return produtos;
    }

    public async Task<List<Produto>?> GetProdutosByCategoriaAsync(Guid categoriaId)
    {
        var produtos = await _context
            .Produtos
            .AsNoTracking()
            .AsQueryable()
            .Include(x => x.Categoria)
            .Where(x => x.CategoriaId == categoriaId)
            .ToListAsync();

        var tamanhos = await _tamanhoRepository.GetTamanhosProdutosAsync() ?? new();

        if (produtos.Count > 0)
        {
            produtos.ForEach(produto =>
            {
                produto.Categoria.Produtos = new();
                produto.Tamanhos = tamanhos
                    .Where(x => x.ProdutoId == produto.Id)
                    .Select(tm =>
                        new Tamanho(tm.Tamanho.Id, tm.Tamanho.DataCriacao, tm.Tamanho.DataAtualizacao, tm.Tamanho.Numero, tm.Tamanho.Descricao)
                     )
                    .ToList();
            });
        }

        return produtos;
    }
}
