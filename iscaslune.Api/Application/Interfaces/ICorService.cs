using iscaslune.Api.Dtos.Cores;
using iscaslune.Api.Model.Cores;

namespace iscaslune.Api.Application.Interfaces;

public interface ICorService
{
    Task<CorViewModel?> CreateCorAsync(CreateCorDto createCorDto);
    Task<CorViewModel?> GetCorByIdAsync(Guid id);
    Task<List<CorViewModel>?> GetCoresAsync(PaginacaoCorDto paginacaoCorDto);
}
