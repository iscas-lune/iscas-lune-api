using iscas_lune_api.Domain.Entities;
using iscaslune.Api.Infrastructure.Interfaces;

namespace iscas_lune_api.Infrastructure.Interfaces;

public interface IPedidosEmAbertoRepository
{
    Task<PedidosEmAberto?> GetFirstOrDefautlAsync();
    Task DeleteAsync(PedidosEmAberto pedidosEmAberto);
    Task AddAsync(PedidosEmAberto pedidosEmAberto);
}
