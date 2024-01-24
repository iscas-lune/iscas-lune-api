using iscaslune.Api.Application.Interfaces;
using iscaslune.Api.Domain.Context;
using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Infrastructure.Interfaces;
using iscaslune.Api.Infrastructure.Repositories;

namespace iscas_lune_api.Infrastructure.Cached;

public class ProdutoCached : GenericRepository<Produto>, IProdutoRepository
{
    private readonly ICachedService<Produto> _cachedService;
    private readonly ProdutoRepository _produtoRepository;

    public ProdutoCached(IscasLuneContext context, ICachedService<Produto> cachedService, ProdutoRepository produtoRepository) : base(context)
    {
        _cachedService = cachedService;
        _produtoRepository = produtoRepository;
    }

    public async Task<Produto?> GetProdutoByIdAsync(Guid id)
    {
        var key = $"produto-{id}";

        var produto = await _cachedService.GetItemAsync(key);

        if (produto == null)
        {
            produto = await _produtoRepository.GetProdutoByIdAsync(id);
            if (produto != null)
                await _cachedService.SetItemAsync(key, produto);
        }

        return produto;
    }

    public async Task<(List<Produto>? produtos, int totalPage)> GetProdutosAsync(int page)
    {
        var key = $"produtos-{page}";

        var produtos = await _cachedService.GetListItemAsync(key);

        if (produtos == null)
        {
            var values = await _produtoRepository.GetProdutosAsync(page);
            if (values.produtos?.Count > 0)
            {
                produtos = values.produtos;
                await _cachedService.SetListItemAsync(key, values.produtos);
            }

        }

        var count = await _produtoRepository.GetTotalPageProdutosAsync();

        return (produtos, count);
    }

    public async Task<List<Produto>> GetProdutosByCarrinhoAsync(List<Guid> produtosIds)
    {
        return await _produtoRepository.GetProdutosByCarrinhoAsync(produtosIds);
    }

    public async Task<List<Produto>?> GetProdutosByCategoriaAsync(Guid categoriaId)
    {
        var key = $"produtos-categoria-{categoriaId}";
        var produtos = await _cachedService.GetListItemAsync(key);

        if (produtos == null)
        {
            produtos = await _produtoRepository.GetProdutosByCategoriaAsync(categoriaId);
            if (produtos?.Count > 0)
                await _cachedService.SetListItemAsync(key, produtos);
        }

        return produtos;
    }
}
