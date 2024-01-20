using iscas_lune_api.Domain.Entities;
using iscas_lune_api.Infrastructure.Interfaces;
using iscas_lune_api.Infrastructure.Repositories;
using iscaslune.Api.Application.Interfaces;

namespace iscas_lune_api.Infrastructure.Cached;

public class PrecoProdutoCached : IPrecoProdutoRepository
{
    private readonly PrecoProdutoRepository _precoProdutoRepository;
    private readonly ICachedService<PrecoProduto> _cached;

    public PrecoProdutoCached(PrecoProdutoRepository precoProdutoRepository, ICachedService<PrecoProduto> cached)
    {
        _precoProdutoRepository = precoProdutoRepository;
        _cached = cached;
    }

    public async Task<List<PrecoProduto>> GetPrecosProdutosAsync()
    {
        var key = "precos-produtos";

        var precosProdutos = await _cached.GetListItemAsync(key);

        if(precosProdutos == null)
        {
            precosProdutos = await _precoProdutoRepository.GetPrecosProdutosAsync();
            if (precosProdutos != null && precosProdutos.Count > 0)
                await _cached.SetListItemAsync(key, precosProdutos);
        }

        return precosProdutos ?? new();
    }
}
