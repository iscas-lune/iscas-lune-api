using iscas_lune_api.Application.Interfaces;
using iscas_lune_api.Dtos.Carrinhos;
using iscas_lune_api.Model.Carrinho;
using iscas_lune_api.Model.PrecosProdutos;
using iscaslune.Api.Infrastructure.Interfaces;
using iscaslune.Api.Model.Categorias;
using iscaslune.Api.Model.Cores;
using iscaslune.Api.Model.Produtos;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
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
    public async Task<bool> AdicionarProdutoAsync(AddCarrinhoDto addCarrinhoDto)
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

        var addProduto = carrinho?.Produtos.FirstOrDefault(x => x.ProdutoId == addCarrinhoDto.ProdutoId);

        if(addProduto == null)
        {
            addProduto = new() 
            {
                ProdutoId = addCarrinhoDto.ProdutoId,
                Pesos = addCarrinhoDto.Pesos,
                Tamanhos = addCarrinhoDto.Tamanhos
            };

            carrinho?.Produtos.Add(addProduto);
        }
        else
        {
            foreach (var pesoCarrinho in addCarrinhoDto.Pesos)
            {
                var peso = addProduto.Pesos.FirstOrDefault(ps => ps.PesoId == pesoCarrinho.PesoId);
                if(peso == null)
                {
                    peso = new() 
                    {
                        PesoId = pesoCarrinho.PesoId,
                        Quantidade = pesoCarrinho.Quantidade
                    };

                    addProduto.Pesos.Add(peso);
                }
                else
                {
                    peso.Quantidade += pesoCarrinho.Quantidade;
                }
            }

            foreach (var tamanhoCarrinho in addCarrinhoDto.Tamanhos)
            {
                var tamanho = addProduto.Tamanhos.FirstOrDefault(tm => tm.TamanhoId == tamanhoCarrinho.TamanhoId);
                
                if(tamanho == null)
                {
                    tamanho = new()
                    {
                        TamanhoId = tamanhoCarrinho.TamanhoId,
                        Quantidade = tamanhoCarrinho.Quantidade
                    };

                    addProduto.Tamanhos.Add(tamanho);   
                }
                else
                {
                    tamanho.Quantidade += tamanhoCarrinho.Quantidade;
                }
            }
        }

        await _distributedCache.SetStringAsync(key, JsonSerializer.Serialize(carrinho), _options);
        return true;
    }

    public async Task<List<CarrinhoViewModel>> GetCarrinhoAsync()
    {
        var carrinhosViewModels = new List<CarrinhoViewModel>();

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
        var produtosIds = carrinho
                .Produtos
                .Select(x => x.ProdutoId).ToList();
        var produtos = await _produtoRepository
            .GetProdutosByCarrinhoAsync(produtosIds);

        foreach (var produto in produtos)
        {
            var carrinhoViewModel = new CarrinhoViewModel()
            {
                Categoria = new CategoriaViewModel().ForModel(produto.Categoria),
                CategoriaId = produto.CategoriaId,
                Descricao = produto.Descricao,
                EspecificacaoTecnica = produto.EspecificacaoTecnica,
                Foto = Encoding.UTF8.GetString(produto.Foto),
                Id = produto.Id,
                Referencia = produto.Referencia,
                Numero = produto.Numero
            };

            carrinhoViewModel.Tamanhos = produto.Tamanhos.OrderBy(x => x.Numero).Select(x => new TamanhoCarrinhoViewModel()
            {
                Id = x.Id,
                Descricao = x.Descricao,
                Numero = x.Numero,
                PrecoProduto = new PrecoProdutoCarrinhoViewModel()
                {
                    Id = x.PrecoProduto.Id,
                    Preco = x.PrecoProduto.Preco,
                    PrecoCusto = x.PrecoProduto.PrecoCusto,
                    PrecoPromocional = x.PrecoProduto.PrecoPromocional,
                    Quantidade = (decimal)(carrinho?
                        .Produtos?
                        .FirstOrDefault(pr => pr.ProdutoId == produto.Id)?
                            .Tamanhos.FirstOrDefault(tm => tm.TamanhoId == x.Id)?
                                .Quantidade ?? 0)
                }
            }).ToList();

            carrinhoViewModel.Pesos = produto.Pesos.OrderBy(x => x.Numero).Select(x => new PesoCarrinhoViewModel() 
            { 
                Id = x.Id,
                Descricao = x.Descricao,
                Numero = x.Numero,
                PrecoProduto = new PrecoProdutoCarrinhoViewModel()
                {
                    Preco = x.PrecoProdutoPeso.Preco,
                    Id = x.PrecoProdutoPeso.Id,
                    PrecoCusto = x.PrecoProdutoPeso.PrecoCusto,
                    PrecoPromocional = x.PrecoProdutoPeso.PrecoPromocional,
                    Quantidade = (decimal)(carrinho?
                        .Produtos?
                        .FirstOrDefault(pr => pr.ProdutoId == produto.Id)?
                            .Pesos.FirstOrDefault(ps => ps.PesoId == x.Id)?
                                .Quantidade ?? 0)
                }
            }).ToList();

            carrinhosViewModels.Add(carrinhoViewModel);
        }

        return carrinhosViewModels;
    }

    public async Task<int> GetCountCarrinhoAsync()
    {
        var claims = _tokenService.GetClaims();
        var key = $"carrinho-{claims.Id}";
        var carrinhoString = await _distributedCache.GetStringAsync(key);
        if (carrinhoString == null) return 0;

        var carrinho = JsonSerializer.Deserialize<CarrinhoModel>(carrinhoString);
        if (carrinho == null) return 0;
        return carrinho.Produtos.Select(x => x.ProdutoId).ToList().Count;
    }
}
