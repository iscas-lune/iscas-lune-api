using iscas_lune_api.Infrastructure.Interfaces;
using iscas_lune_api.Infrastructure.Repositories;
using iscaslune.Api.Application.Interfaces;
using iscaslune.Api.Domain.Context;
using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Infrastructure.Repositories;

namespace iscas_lune_api.Infrastructure.Cached;

public class TamanhosProdutosCached : GenericRepository<TamanhosProdutos>, ITamanhoProdutoRepository
{
    private readonly ICachedService<TamanhosProdutos> _cachedService;
    private readonly TamanhoProdutoRepository _repository;
    public TamanhosProdutosCached(IscasLuneContext context, ICachedService<TamanhosProdutos> cachedService, TamanhoProdutoRepository repository) : base(context)
    {
        _cachedService = cachedService;
        _repository = repository;
    }

    public async Task<List<TamanhosProdutos>?> GetTamanhosProdutosAsync()
    {
        var key = "tamanhos-produtos";
        var tamanhosProduto = await _cachedService.GetListItemAsync(key);

        if (tamanhosProduto == null)
        {
            tamanhosProduto = await _repository.GetTamanhosProdutosAsync();
            if (tamanhosProduto != null && tamanhosProduto.Count > 0)
                await _cachedService.SetListItemAsync(key, tamanhosProduto);
        }

        return tamanhosProduto;
    }
}
