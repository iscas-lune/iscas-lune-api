using iscas_lune_api.Infrastructure.Interfaces;
using iscaslune.Api.Domain.Context;
using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Infrastructure.Repositories;

namespace iscas_lune_api.Infrastructure.Repositories;

public class TamanhoProdutoRepository : GenericRepository<TamanhosProdutos>, ITamanhoProdutoRepository
{
    public TamanhoProdutoRepository(IscasLuneContext context) : base(context)
    {
    }
}
