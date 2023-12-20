using iscaslune.Api.Application.Interfaces;
using iscaslune.Api.Dtos.Cores;
using iscaslune.Api.Infrastructure.Interfaces;
using iscaslune.Api.Model.Cores;

namespace iscaslune.Api.Application.Services;

public class CorService
    : ICorService
{
    private readonly ICorRepository _corRepository;

    public CorService(ICorRepository corRepository)
    {
        _corRepository = corRepository;
    }

    public async Task<PesoViewModel?> CreateCorAsync(CreatePesoDto createCorDto)
    {
        var cor = createCorDto.ForEntity();
        var result = await _corRepository.AddAsync(cor);

        if(!result) return null;

        return new PesoViewModel().ForModel(cor);
    }

    public async Task<PesoViewModel?> GetCorByIdAsync(Guid id)
    {
        var cor = await _corRepository.GetCorByIdAsync(id);
        return new PesoViewModel().ForModel(cor);
    }

    public async Task<List<PesoViewModel>?> GetCoresAsync(PaginacaoPesoDto paginacaoCorDto)
    {
        var cores = await _corRepository.GetCoresAsync(paginacaoCorDto);
        return cores?.Select(x => new PesoViewModel().ForModel(x) ?? new()).ToList();
    }
}
