using iscaslune.Api.Dtos.Categorias;
using iscaslune.Api.Model.Categorias;

namespace iscaslune.Api.Application.Interfaces;

public interface ICategoriaService
{
    Task<CategoriaViewModel?> CreateCategoriaAsync(CreateCategoriaDto createCategoriaDto);
    Task<CategoriaViewModel?> GetCategoriaByIdAsync(Guid id);
    Task<List<CategoriaViewModel>?> GetCategoriasAsync(PaginacaoCategoriaDto paginaCategoriaDto);
}
