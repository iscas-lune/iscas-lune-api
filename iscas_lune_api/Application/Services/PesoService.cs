using iscaslune.Api.Application.Interfaces;
using iscaslune.Api.Dtos.Cores;
using iscaslune.Api.Infrastructure.Interfaces;
using iscaslune.Api.Model.Cores;

namespace iscaslune.Api.Application.Services;

public class PesoService
    : IPesoService
{
    private readonly IPesoRepository _pesoRepository;

    public PesoService(IPesoRepository pesoRepository)
    {
        _pesoRepository = pesoRepository;
    }

    public async Task<PesoViewModel?> CreateCorAsync(CreatePesoDto createCorDto)
    {
        var cor = createCorDto.ForEntity();
        var result = await _pesoRepository.AddAsync(cor);

        if(!result) return null;

        return new PesoViewModel().ForModel(cor);
    }

    public async Task<PesoViewModel?> GetCorByIdAsync(Guid id)
    {
        var cor = await _pesoRepository.GetPesoByIdAsync(id);
        return new PesoViewModel().ForModel(cor);
    }

    public async Task<List<PesoViewModel>?> GetCoresAsync(PaginacaoPesoDto paginacaoCorDto)
    {
        var cores = await _pesoRepository.GetPesosAsync(paginacaoCorDto);
        return cores?.Select(x => new PesoViewModel().ForModel(x) ?? new()).ToList();
    }
}
