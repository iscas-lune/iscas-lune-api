using iscas_lune_api.Application.Interfaces;
using iscas_lune_api.Model.Carrinho;
using iscaslune.Api.Infrastructure.Interfaces;
using iscaslune.Api.Model.Produtos;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace iscas_lune_api.Application.Services;

public class CarrinhoService : ICarrinhoService
{
    private readonly IDistributedCache _distributedCache;
    private readonly IProdutoRepository _produtoRepository;
    private readonly ITokenService _tokenService;
    private readonly DistributedCacheEntryOptions _options;
    private readonly JsonSerializerOptions _serializerOptions;
    private static readonly double _absolutExpiration =
        double.Parse(Environment.GetEnvironmentVariable("EXPIRACAO_ABSOLUTA_CARRINHO") ?? "24");
    private static readonly double _slidingExpiration =
        double.Parse(Environment.GetEnvironmentVariable("EXPIRACAO_SLIDING_CARRINHO") ?? "12");
    public CarrinhoService(IDistributedCache distributedCache, ITokenService tokenService, IProdutoRepository produtoRepository)
    {
        _serializerOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = ReferenceHandler.Preserve
        };
        _options = new DistributedCacheEntryOptions()
                      .SetAbsoluteExpiration(TimeSpan.FromHours(_absolutExpiration))
                      .SetSlidingExpiration(TimeSpan.FromHours(_slidingExpiration));

        _distributedCache = distributedCache;
        _tokenService = tokenService;
        _produtoRepository = produtoRepository;
    }
    public async Task<bool> AdicionarProdutoAsync(Guid produtoId)
    {
        var claims = _tokenService.GetClaims();
        var key = $"carrinho-{claims.Id}";
        var carrinhoString = await _distributedCache.GetStringAsync(key);
        var carrinho = new CarrinhoModel
        {
            UsuarioId = claims.Id
        };

        if (carrinhoString != null)
        {
            carrinho = JsonSerializer.Deserialize<CarrinhoModel>(carrinhoString, _serializerOptions)
                ?? new() { UsuarioId = claims.Id };
            await _distributedCache.RemoveAsync(key);
        }

        var newProduto = carrinho?.ProdutosIds.FirstOrDefault(x => x == produtoId);
        if (newProduto == null || newProduto == Guid.Empty) carrinho?.ProdutosIds.Add(produtoId);
        await _distributedCache.SetStringAsync(key, JsonSerializer.Serialize(carrinho), _options);
        return true;

    }

    public async Task<List<ProdutoViewModel>> GetCarrinhoAsync()
    {
        var claims = _tokenService.GetClaims();
        var carrinho = new CarrinhoModel
        {
            UsuarioId = claims.Id
        };
        var carrinhoString = await _distributedCache.GetStringAsync($"carrinho-{claims.Id}");
        if (carrinhoString != null)
        {
            carrinho = JsonSerializer.Deserialize<CarrinhoModel>(carrinhoString, _serializerOptions)
                ?? new() { UsuarioId = claims.Id };
        }

        var produtos = await _produtoRepository.GetProdutosByCarrinhoAsync(carrinho.ProdutosIds);

        return produtos.Select(x => new ProdutoViewModel().ForModel(x) ?? new()).ToList();
    }

    public async Task<int> GetCountCarrinhoAsync()
    {
        var claims = _tokenService.GetClaims();
        var key = $"carrinho-{claims.Id}";
        var carrinhoString = await _distributedCache.GetStringAsync(key);
        if (carrinhoString == null) return 0;

        var carrinho = JsonSerializer.Deserialize<CarrinhoModel>(carrinhoString);
        if (carrinho == null) return 0;
        return carrinho.ProdutosIds.Count;
    }
}
