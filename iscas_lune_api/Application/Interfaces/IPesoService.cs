using iscaslune.Api.Dtos.Cores;
using iscaslune.Api.Model.Cores;

namespace iscaslune.Api.Application.Interfaces;

public interface ICorService
{
    Task<PesoViewModel?> CreateCorAsync(CreatePesoDto createCorDto);
    Task<PesoViewModel?> GetCorByIdAsync(Guid id);
    Task<List<PesoViewModel>?> GetCoresAsync(PaginacaoPesoDto paginacaoCorDto);
}
