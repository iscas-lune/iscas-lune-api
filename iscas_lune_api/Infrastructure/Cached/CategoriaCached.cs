using iscaslune.Api.Application.Interfaces;
using iscaslune.Api.Domain.Context;
using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Dtos.Categorias;
using iscaslune.Api.Infrastructure.Interfaces;
using iscaslune.Api.Infrastructure.Repositories;

namespace iscaslune.Api.Infrastructure.Cached;

public class CategoriaCached
    : GenericRepository<Categoria>, ICategoriaRepository
{
    private readonly CategoriaRepository _categoriaRepository;
    private readonly ICachedService<Categoria> _cachedService;
    private const string _keyList = "categorias";

    public CategoriaCached(IscasLuneContext context, CategoriaRepository categoriaRepository, ICachedService<Categoria> cachedService) : base(context)
    {
        _categoriaRepository = categoriaRepository;
        _cachedService = cachedService;
    }

    public async Task<Categoria?> GetCategoriaByIdAsync(Guid id)
    {
        var key = id.ToString();
        var categoria = await _cachedService.GetItemAsync(key);

        if(categoria == null)
        {
            categoria = await _categoriaRepository.GetCategoriaByIdAsync(id);
            if(categoria != null)
            {
                categoria.Produtos.ForEach(x =>
                {
                    x.Categoria = null;
                });

                await _cachedService.SetItemAsync(key, categoria);
            }
        }

        return categoria;
    }

    public async Task<List<Categoria>?> GetCategoriasAsync(PaginacaoCategoriaDto paginacaoCategoriaDto)
    {
        if (!string.IsNullOrWhiteSpace(paginacaoCategoriaDto.Descricao)
            || paginacaoCategoriaDto.Asc)
            return await _categoriaRepository.GetCategoriasAsync(paginacaoCategoriaDto);

        var categorias = await _cachedService.GetListItemAsync(_keyList);

        if(categorias == null)
        {
            categorias = await _categoriaRepository.GetCategoriasAsync(paginacaoCategoriaDto);

            if(categorias != null && categorias.Count > 0)
            {
                categorias.ForEach(categoria =>
                {
                    categoria.Produtos.ForEach(produto =>
                    {
                        produto.Categoria = null;
                    });
                });
                await _cachedService.SetListItemAsync(_keyList, categorias);
            }
                
        }

        return categorias;
    }
}
