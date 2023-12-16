using iscas_lune_api.Application.Interfaces;
using iscas_lune_api.Dtos.Tamanhos;
using iscas_lune_api.Infrastructure.Interfaces;
using iscaslune.Api.Domain.Entities;

namespace iscas_lune_api.Application.Services;

public class TamanhoProdutoService : ITamanhoProdutoService
{
    private readonly ITamanhoProdutoRepository _repository;

    public TamanhoProdutoService(ITamanhoProdutoRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> CreateTamanhoProdutoAsync(CreateTamanhoProdutoDto createTamanhoProdutoDto)
    {
        var tamanhoProduto = new TamanhosProdutos(Guid.NewGuid(), createTamanhoProdutoDto.ProdutoId, createTamanhoProdutoDto.TamanhoId);
        return await _repository.AddAsync(tamanhoProduto);
    }
}
