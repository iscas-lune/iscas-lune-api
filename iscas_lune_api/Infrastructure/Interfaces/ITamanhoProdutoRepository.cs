using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Infrastructure.Interfaces;

namespace iscas_lune_api.Infrastructure.Interfaces;

public interface ITamanhoProdutoRepository : IGenericRepository<TamanhosProdutos>
{
    Task<List<TamanhosProdutos>?> GetTamanhosProdutosAsync();
}
