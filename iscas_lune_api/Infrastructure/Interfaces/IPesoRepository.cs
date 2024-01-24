using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Dtos.Cores;

namespace iscaslune.Api.Infrastructure.Interfaces;

public interface IPesoRepository : IGenericRepository<Peso>
{
    Task<Peso?> GetPesoByIdAsync(Guid id);
    Task<List<Peso>> GetPesosAsync(PaginacaoPesoDto filterModel);
}
