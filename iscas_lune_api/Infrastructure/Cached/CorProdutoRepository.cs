using iscas_lune_api.Infrastructure.Interfaces;
using iscaslune.Api.Domain.Context;
using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Infrastructure.Repositories;

namespace iscas_lune_api.Infrastructure.Cached;

public class CorProdutoRepository : GenericRepository<CoresProdutos>, ICorProdutoRepository
{
    public CorProdutoRepository(IscasLuneContext context) : base(context)
    {
    }
}
