using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Dtos.Cores;

namespace iscaslune.Api.Infrastructure.Interfaces;

public interface ICorRepository : IGenericRepository<Cor>
{
    Task<Cor?> GetCorByIdAsync(Guid id);
    Task<List<Cor>> GetCoresAsync(PaginacaoCorDto filterModel);
}
