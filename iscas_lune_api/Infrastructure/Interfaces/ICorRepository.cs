using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Dtos.Cores;

namespace iscaslune.Api.Infrastructure.Interfaces;

public interface ICorRepository : IGenericRepository<Peso>
{
    Task<Peso?> GetCorByIdAsync(Guid id);
    Task<List<Peso>> GetCoresAsync(PaginacaoPesoDto filterModel);
}
