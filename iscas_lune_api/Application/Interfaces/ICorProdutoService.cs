using iscas_lune_api.Dtos.Cores;

namespace iscas_lune_api.Application.Interfaces;

public interface ICorProdutoService
{
    Task<bool> CreateCorProdutoAsync(CreateCorProdutoDto createCorProdutoDto);
}
