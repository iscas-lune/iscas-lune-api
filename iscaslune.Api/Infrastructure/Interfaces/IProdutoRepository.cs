using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Dtos.Produtos;

namespace iscaslune.Api.Infrastructure.Interfaces;

public interface IProdutoRepository : IGenericRepository<Produto>
{
    Task<Produto?> GetProdutoByIdAsync(Guid id);
    Task<List<Produto>?> GetProdutosAsync(PaginacaoProdutoDto paginacaoProduto);
}
