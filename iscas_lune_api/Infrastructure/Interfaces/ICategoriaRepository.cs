using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Dtos.Categorias;

namespace iscaslune.Api.Infrastructure.Interfaces;

public interface ICategoriaRepository : IGenericRepository<Categoria>
{
    Task<Categoria?> GetCategoriaByIdAsync(Guid id);
    Task<List<Categoria>?> GetCategoriasAsync(PaginacaoCategoriaDto paginacaoCategoriaDto);
}
