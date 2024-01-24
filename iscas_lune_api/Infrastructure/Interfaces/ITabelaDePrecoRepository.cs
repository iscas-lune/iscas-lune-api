using iscas_lune_api.Domain.Entities;
using iscaslune.Api.Infrastructure.Interfaces;

namespace iscas_lune_api.Infrastructure.Interfaces;

public interface ITabelaDePrecoRepository : IGenericRepository<TabelaDePreco>
{
    Task<TabelaDePreco?> GetTabelaDePrecoAtivaEcommerceAsync();
}
