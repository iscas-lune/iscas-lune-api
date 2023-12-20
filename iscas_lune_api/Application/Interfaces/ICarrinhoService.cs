using iscaslune.Api.Model.Produtos;

namespace iscas_lune_api.Application.Interfaces;

public interface ICarrinhoService
{
    Task<bool> AdicionarProdutoAsync(Guid produtoId);
    Task<List<ProdutoViewModel>> GetCarrinhoAsync();
    Task<int> GetCountCarrinhoAsync();
}
