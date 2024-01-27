using iscas_lune_api.Application.Interfaces;
using iscas_lune_api.Dtos.Carrinhos;
using iscas_lune_api.Exceptions;
using iscas_lune_api.Model.Carrinho;
using iscas_lune_api.Model.Usuarios;
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
    public CarrinhoService(
        IDistributedCache distributedCache,
        ITokenService tokenService,
        IProdutoRepository produtoRepository)
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
        var (carrinho, key) = await GetCarrinhoUsuarioAsync();
        var addProduto = carrinho?
            .Produtos
            .FirstOrDefault(x => x.ProdutoId == addCarrinhoDto.ProdutoId);

        if (addProduto == null)
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
            AddPesosCarrinho(addCarrinhoDto.Pesos, addProduto);
            AddTamanhosCarrinho(addCarrinhoDto.Tamanhos, addProduto);
        }

        await SetCarrinhoAsync(carrinho ?? new(), key);

        return true;
    }

    public async Task<List<CarrinhoViewModel>> GetCarrinhoAsync()
    {
        var carrinhosViewModels = new List<CarrinhoViewModel>();

        var (carrinho, key) = await GetCarrinhoUsuarioAsync();

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
                PrecoProduto = new QuantidadeProdutoCarrinhoViewModel()
                {
                    Quantidade = (decimal)(carrinho?
                        .Produtos?
                        .FirstOrDefault(pr => pr.ProdutoId == produto.Id)?
                            .Tamanhos.FirstOrDefault(ps => ps.TamanhoId == x.Id)?
                                .Quantidade ?? 0)
                }
            }).ToList();

            carrinhoViewModel.Pesos = produto.Pesos.OrderBy(x => x.Numero).Select(x => new PesoCarrinhoViewModel()
            {
                Id = x.Id,
                Descricao = x.Descricao,
                Numero = x.Numero,
                PrecoProduto = new QuantidadeProdutoCarrinhoViewModel()
                {
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
        var (carrinho, key) = await GetCarrinhoUsuarioAsync();

        if (carrinho == null) return 0;

        return carrinho.Produtos.Select(x => x.ProdutoId).ToList().Count;
    }
    private static void AddPesosCarrinho(List<AddPesoCarrinho> pesos, AddCarrinhoDto addCarrinhoDto)
    {
        foreach (var pesoCarrinho in pesos)
        {
            var peso = addCarrinhoDto
                .Pesos
                .FirstOrDefault(ps => ps.PesoId == pesoCarrinho.PesoId);

            if (peso == null)
            {
                peso = new()
                {
                    PesoId = pesoCarrinho.PesoId,
                    Quantidade = pesoCarrinho.Quantidade
                };

                addCarrinhoDto.Pesos.Add(peso);
            }
            else
            {
                peso.Quantidade += pesoCarrinho.Quantidade;
            }
        }
    }
    private static void AddTamanhosCarrinho(List<AddTamanhoCarrinho> tamanhos, AddCarrinhoDto addProduto)
    {
        foreach (var tamanhoCarrinho in tamanhos)
        {
            var tamanho = addProduto.Tamanhos.FirstOrDefault(tm => tm.TamanhoId == tamanhoCarrinho.TamanhoId);

            if (tamanho == null)
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

    public async Task<bool> DeleteProdutoCarrinhoAsync(Guid produtoId)
    {
        var (carrinho, key) = await GetCarrinhoUsuarioAsync();

        var produto = carrinho.Produtos.FirstOrDefault(x => x.ProdutoId == produtoId);

        if (produto != null)
        {
            carrinho.Produtos.Remove(produto);
            await SetCarrinhoAsync(carrinho, key);
        }

        return true;
    }

    private UsuarioViewModel GetClaimsUsuario()
    {
        return _tokenService.GetClaims();
    }

    private string GetKeyCarrinhoUsuario()
    {
        var claims = GetClaimsUsuario();
        return $"carrinho-{claims.Id}";
    }

    private async Task SetCarrinhoAsync(CarrinhoModel carrinho, string key)
    {
        await _distributedCache
        .SetStringAsync(key,
        JsonSerializer.Serialize(carrinho), _options);
    }

    private async Task<(CarrinhoModel carrinho, string key)> GetCarrinhoUsuarioAsync()
    {
        var claims = GetClaimsUsuario();
        var key = GetKeyCarrinhoUsuario();
        var carrinhoString = await _distributedCache.GetStringAsync(key);
        var carrinho = new CarrinhoModel
        {
            UsuarioId = claims.Id
        };

        if (carrinhoString != null)
        {
            carrinho = JsonSerializer.Deserialize<CarrinhoModel>(carrinhoString, _serializerOptions)
                ?? new() { UsuarioId = claims.Id };
        }

        if (carrinho == null)
            throw new ExceptionApi("Não foi possível localizar o seu carrinho!");

        return (carrinho, key);
    }
}
