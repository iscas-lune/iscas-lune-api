using iscaslune.Api.Application.Interfaces;
using iscaslune.Api.Dtos.Categorias;
using iscaslune.Api.Infrastructure.Interfaces;
using iscaslune.Api.Model.Categorias;

namespace iscaslune.Api.Application.Services;

public class CategoriaService(ICategoriaRepository categoriaRepository)
    : ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;

    public async Task<CategoriaViewModel?> CreateCategoriaAsync(CreateCategoriaDto createCategoriaDto)
    {
        var categoria = createCategoriaDto.ForEntity();
        var result = await _categoriaRepository.AddAsync(categoria);
        if (!result) return null;
        return new CategoriaViewModel().ForModel(categoria);
    }

    public async Task<CategoriaViewModel?> GetCategoriaByIdAsync(Guid id)
    {
        var categoria = await _categoriaRepository.GetCategoriaByIdAsync(id);
        return new CategoriaViewModel().ForModel(categoria);
    }

    public async Task<List<CategoriaViewModel>?> GetCategoriasAsync(PaginacaoCategoriaDto paginaCategoriaDto)
    {
        var categorias = await _categoriaRepository.GetCategoriasAsync(paginaCategoriaDto);
        return categorias?.Select(x => new CategoriaViewModel().ForModel(x) ?? new()).ToList();
    }
}
