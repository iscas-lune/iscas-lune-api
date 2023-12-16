using iscaslune.Api.Application.Interfaces;
using iscaslune.Api.Dtos.Produtos;
using iscaslune.Api.Infrastructure.Interfaces;
using iscaslune.Api.Model.Produtos;

namespace iscaslune.Api.Application.Services;

public class ProdutoService
    : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<ProdutoViewModel?> CreateProdutoAsync(CreateProdutoDto createProdutoDto)
    {
        var produto = createProdutoDto.ForEntity();
        var result = await _produtoRepository.AddAsync(produto);
        if (!result) return null;
        return new ProdutoViewModel().ForModel(produto);
    }

    public async Task<ProdutoViewModel?> GetProdutoByIdAsync(Guid id)
    {
        var produto = await _produtoRepository.GetProdutoByIdAsync(id);
        return new ProdutoViewModel().ForModel(produto);
    }

    public async Task<List<ProdutoViewModel>?> GetProdutosAsync(PaginacaoProdutoDto paginacaoProdutoDto)
    {
        var produtos = await _produtoRepository.GetProdutosAsync(paginacaoProdutoDto);
        return produtos?.Select(x => new ProdutoViewModel().ForModel(x) ?? new()).ToList();
    }

    public async Task<List<ProdutoViewModel>?> GetProdutosByCategoriaAsync(Guid categoriaId)
    {
        var produtos = await _produtoRepository.GetProdutosByCategoriaAsync(categoriaId);
        return produtos?.Select(x => new ProdutoViewModel().ForModel(x) ?? new()).ToList();
    }
}
