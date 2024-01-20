using iscas_lune_api.Domain.Entities;

namespace iscas_lune_api.Infrastructure.Interfaces;

public interface IPrecoProdutoRepository
{
    Task<List<PrecoProduto>> GetPrecosProdutosAsync();
}
