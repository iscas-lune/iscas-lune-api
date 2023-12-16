using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Dtos.Tamanhos;

namespace iscaslune.Api.Infrastructure.Interfaces;

public interface ITamanhoRepository : IGenericRepository<Tamanho>
{
    Task<Tamanho?> GetTamanhoByIdAsync(Guid id);
    Task<List<Tamanho>?> GetTamanhosAsync(PaginacaoTamanhoDto paginacaoTamanhoDto);
}
