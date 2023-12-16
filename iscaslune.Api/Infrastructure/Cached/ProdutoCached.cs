using iscaslune.Api.Application.Interfaces;
using iscaslune.Api.Domain.Context;
using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Dtos.Produtos;
using iscaslune.Api.Infrastructure.Interfaces;
using iscaslune.Api.Infrastructure.Repositories;

namespace iscaslune.Api.Infrastructure.Cached;

public class ProdutoCached(IscasLuneContext context, ProdutoRepository produtoRepository, ICachedService<Produto> cachedService) 
    : GenericRepository<Produto>(context), IProdutoRepository
{
    private readonly ProdutoRepository _produtoRepository = produtoRepository;
    private readonly ICachedService<Produto> _cachedService = cachedService;
    private const string _keyList = "produtos";

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
                produtos.ForEach(x =>
                {
                    x.Categoria.Produtos = new();
                });

                await _cachedService.SetListItemAsync(_keyList, produtos);
            }

        }

        return produtos;
    }
}
