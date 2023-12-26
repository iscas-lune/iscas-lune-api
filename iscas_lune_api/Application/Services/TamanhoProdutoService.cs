using iscas_lune_api.Application.Interfaces;
using iscas_lune_api.Dtos.Tamanhos;
using iscas_lune_api.Infrastructure.Interfaces;
using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Dtos.Tamanhos;
using iscaslune.Api.Infrastructure.Interfaces;

namespace iscas_lune_api.Application.Services;

public class TamanhoProdutoService : ITamanhoProdutoService
{
    private readonly ITamanhoProdutoRepository _repository;
    private readonly ITamanhoRepository _tamanhoRepository;

    public TamanhoProdutoService(ITamanhoProdutoRepository repository, ITamanhoRepository tamanhoRepository)
    {
        _repository = repository;
        _tamanhoRepository = tamanhoRepository;
    }

    public async Task<bool> CreateTamanhoProdutoAsync(CreateTamanhoProdutoDto createTamanhoProdutoDto)
    {
        var tamanhos = await _tamanhoRepository.GetTamanhosAsync(new PaginacaoTamanhoDto());

        var tamanhosProdutos = tamanhos?.Select(x => new TamanhosProdutos(Guid.NewGuid(), createTamanhoProdutoDto.ProdutoId, x.Id)).ToList();
        
        return await _repository.AddRangeAsync(tamanhosProdutos ?? new());
    }
}
