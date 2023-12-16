using iscaslune.Api.Dtos.Tamanhos;
using iscaslune.Api.Model.Tamanhos;

namespace iscaslune.Api.Application.Interfaces;

public interface ITamanhoService
{
    Task<TamanhoViewModel?> CreateTamanhoAsync(CreateTamanhoDto createTamanhoDto);
    Task<TamanhoViewModel?> GetTamanhobyIdAsync(Guid id);
    Task<List<TamanhoViewModel>?> GetTamanhosAsync(PaginacaoTamanhoDto paginacaoTamanhoDto);
}
