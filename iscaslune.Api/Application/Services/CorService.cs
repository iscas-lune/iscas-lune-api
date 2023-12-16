using iscaslune.Api.Application.Interfaces;
using iscaslune.Api.Dtos.Cores;
using iscaslune.Api.Infrastructure.Interfaces;
using iscaslune.Api.Model.Cores;

namespace iscaslune.Api.Application.Services;

public class CorService(ICorRepository corRepository) 
    : ICorService
{
    private readonly ICorRepository _corRepository = corRepository;

    public async Task<CorViewModel?> CreateCorAsync(CreateCorDto createCorDto)
    {
        var cor = createCorDto.ForEntity();
        var result = await _corRepository.AddAsync(cor);

        if(!result) return null;

        return new CorViewModel().ForModel(cor);
    }

    public async Task<CorViewModel?> GetCorByIdAsync(Guid id)
    {
        var cor = await _corRepository.GetCorByIdAsync(id);
        return new CorViewModel().ForModel(cor);
    }

    public async Task<List<CorViewModel>?> GetCoresAsync(PaginacaoCorDto paginacaoCorDto)
    {
        var cores = await _corRepository.GetCoresAsync(paginacaoCorDto);
        return cores?.Select(x => new CorViewModel().ForModel(x) ?? new()).ToList();
    }
}
