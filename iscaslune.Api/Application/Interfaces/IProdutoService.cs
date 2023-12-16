using iscaslune.Api.Dtos.Produtos;
using iscaslune.Api.Model.Produtos;

namespace iscaslune.Api.Application.Interfaces;

public interface IProdutoService
{
    Task<ProdutoViewModel?> CreateProdutoAsync(CreateProdutoDto createProdutoDto);
    Task<ProdutoViewModel?> GetProdutoByIdAsync(Guid id);
    Task<List<ProdutoViewModel>?> GetProdutosAsync(PaginacaoProdutoDto paginacaoProdutoDto);
}
