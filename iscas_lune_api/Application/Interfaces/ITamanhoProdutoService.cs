using iscas_lune_api.Dtos.Tamanhos;

namespace iscas_lune_api.Application.Interfaces;

public interface ITamanhoProdutoService
{
    Task<bool> CreateTamanhoProdutoAsync(CreateTamanhoProdutoDto createTamanhoProdutoDto);
}
