using iscaslune.Api.Application.Interfaces;
using iscaslune.Api.Dtos.Categorias;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace iscaslune.Api.Controllers;

[ApiController]
[Route("api/categoria")]
public class CategoriaController(ICategoriaService categoriaService) : ControllerBaseIscasLune
{
    private readonly ICategoriaService _categoriaService = categoriaService;

    [HttpPost("create")]
    public async Task<IActionResult> CreateCategoria(CreateCategoriaDto createCategoriaDto)
    {
        try
        {
            var result = await _categoriaService.CreateCategoriaAsync(createCategoriaDto);
            return HandleCreated(result, $"api/categoria/get?id={result?.Id}");
        }
        catch (Exception ex)
        {
            return HandleError(ex.Message);
        }
    }

    [EnableCors("iscasluneorigin")]
    [HttpGet("get")]
    public async Task<IActionResult> GetCategoria([FromQuery] Guid id)
    {
        try
        {
            var result = await _categoriaService.GetCategoriaByIdAsync(id);
            return HandleGet(result);
        }
        catch (Exception ex)
        {
            return HandleError(ex.Message);
        }
    }

    [HttpGet("list")]
    [EnableCors("iscasluneorigin")]
    public async Task<IActionResult> ListCategorias([FromQuery] PaginacaoCategoriaDto paginacaoCategoriaDto)
    {
        try
        {
            var result = await _categoriaService.GetCategoriasAsync(paginacaoCategoriaDto);
            return HandleGet(result);
        }
        catch (Exception ex)
        {
            return HandleError(ex.Message);
        }
    }
}
