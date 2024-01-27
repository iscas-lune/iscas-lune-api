using iscas_lune_api.Dtos.Carrinhos;
using iscas_lune_api.Model.Carrinho;
using iscaslune.Api.Model.Produtos;

namespace iscas_lune_api.Application.Interfaces;

public interface ICarrinhoService
{
    Task<bool> AdicionarProdutoAsync(AddCarrinhoDto addCarrinhoDto);
    Task<List<CarrinhoViewModel>> GetCarrinhoAsync();
    Task<int> GetCountCarrinhoAsync();
    Task<bool> DeleteProdutoCarrinhoAsync(Guid produtoId);
}
