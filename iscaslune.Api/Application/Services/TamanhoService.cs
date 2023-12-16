using iscaslune.Api.Application.Interfaces;
using iscaslune.Api.Dtos.Tamanhos;
using iscaslune.Api.Infrastructure.Interfaces;
using iscaslune.Api.Model.Tamanhos;

namespace iscaslune.Api.Application.Services;

public class TamanhoService(ITamanhoRepository tamanhoRepository)
    : ITamanhoService
{
    private readonly ITamanhoRepository _tamanhoRepository = tamanhoRepository;

    public async Task<TamanhoViewModel?> CreateTamanhoAsync(CreateTamanhoDto createTamanhoDto)
    {
        var tamanho = createTamanhoDto.ForEntity();
        var result = await _tamanhoRepository.AddAsync(tamanho);
        if (!result) throw new Exception("Ocorreu um erro ao adicionar o tamanho");
        return new TamanhoViewModel().ForModel(tamanho);
    }

    public async Task<TamanhoViewModel?> GetTamanhobyIdAsync(Guid id)
    {
        var tamanho = await _tamanhoRepository.GetTamanhoByIdAsync(id);
        return new TamanhoViewModel().ForModel(tamanho);
    }

    public async Task<List<TamanhoViewModel>?> GetTamanhosAsync(PaginacaoTamanhoDto paginacaoTamanhoDto)
    {
        var tamanhos = await _tamanhoRepository.GetTamanhosAsync(paginacaoTamanhoDto);
        return tamanhos?.Select(x => new TamanhoViewModel().ForModel(x) ?? new()).ToList();
    }
}
