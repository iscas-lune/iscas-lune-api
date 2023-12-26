using iscaslune.Api.Application.Interfaces;
using iscaslune.Api.Domain.Context;
using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Dtos.Produtos;
using iscaslune.Api.Infrastructure.Interfaces;
using iscaslune.Api.Infrastructure.Repositories;

namespace iscaslune.Api.Infrastructure.Cached;

public class ProdutoCached
    : GenericRepository<Produto>, IProdutoRepository
{
    private readonly ProdutoRepository _produtoRepository;
    private readonly ICachedService<Produto> _cachedService;
    private const string _keyList = "produtos";

    public ProdutoCached(IscasLuneContext context, ProdutoRepository produtoRepository, ICachedService<Produto> cachedService) : base(context)
    {
        _produtoRepository = produtoRepository;
        _cachedService = cachedService;
    }

    public async Task<Produto?> GetProdutoByIdAsync(Guid id)
    {
        var key = id.ToString();
        var produto = await _cachedService.GetItemAsync(key);

        if(produto == null)
        {
            produto = await _produtoRepository.GetProdutoByIdAsync(id);
            if(produto != null)
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
                
                await _cachedService.SetItemAsync(key, produto);
            }
        }

        return produto;
    }

    public async Task<List<Produto>?> GetProdutosAsync(PaginacaoProdutoDto paginacaoProduto)
    {
        if (!string.IsNullOrWhiteSpace(paginacaoProduto.Descricao)
            || paginacaoProduto.Asc)
            return await _produtoRepository.GetProdutosAsync(paginacaoProduto);

        var produtos = await _cachedService.GetListItemAsync(_keyList);

        if(produtos == null || produtos.Count == 0)
        {
            produtos = await _produtoRepository.GetProdutosAsync(paginacaoProduto);

            if(produtos != null && produtos.Count > 0)
            {
                produtos.ForEach((Action<Produto>)(produto =>
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
                }));

                await _cachedService.SetListItemAsync(_keyList, produtos);
            }

        }

        return produtos;
    }

    public async Task<List<Produto>?> GetProdutosByCategoriaAsync(Guid categoriaId)
    {
        var produtos = await _cachedService.GetListItemAsync($"{_keyList}-{categoriaId}");

        if(produtos == null)
        {
            produtos = await _produtoRepository.GetProdutosByCategoriaAsync(categoriaId);
            if(produtos?.Count > 0)
            {
                produtos.ForEach((Action<Produto>)(produto =>
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
                }));

                await _cachedService.SetListItemAsync(_keyList + categoriaId, produtos);
            }
        }

        return produtos;
    }

    public async Task<List<Produto>> GetProdutosByCarrinhoAsync(List<Guid> produtosIds)
        => await _produtoRepository.GetProdutosByCarrinhoAsync(produtosIds);
}
