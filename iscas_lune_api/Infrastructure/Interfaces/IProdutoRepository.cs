using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Dtos.Produtos;

namespace iscaslune.Api.Infrastructure.Interfaces;

public interface IProdutoRepository : IGenericRepository<Produto>
{
    Task<Produto?> GetProdutoByIdAsync(Guid id);
    Task<(List<Produto>? produtos, int totalPage)> GetProdutosAsync(int page);
    Task<List<Produto>?> GetProdutosByCategoriaAsync(Guid categoriaId);
    Task<List<Produto>> GetProdutosByCarrinhoAsync(List<Guid> produtosIds);
}
