using iscas_lune_api.Application.Interfaces;
using iscas_lune_api.Dtos.Cores;
using iscas_lune_api.Infrastructure.Interfaces;
using iscaslune.Api.Domain.Entities;

namespace iscas_lune_api.Application.Services;

public class CorProdutoService : ICorProdutoService
{
    private readonly ICorProdutoRepository _corProdutoRepository;

    public CorProdutoService(ICorProdutoRepository corProdutoRepository)
    {
        _corProdutoRepository = corProdutoRepository;
    }

    public async Task<bool> CreateCorProdutoAsync(CreateCorProdutoDto createCorProdutoDto)
    {
        var corProduto = new CoresProdutos(Guid.NewGuid(), createCorProdutoDto.ProdutoId, createCorProdutoDto.CorId);
        return await _corProdutoRepository.AddAsync(corProduto);
    }
}
