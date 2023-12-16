using iscas_lune_api.Application.Interfaces;
using iscas_lune_api.Application.Services;
using iscas_lune_api.Dtos.Cores;
using iscas_lune_api.Dtos.Tamanhos;
using iscaslune.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace iscas_lune_api.Controllers;

[ApiController]
[Route("api/tamanho-produto")]
public class TamanhoProdutoController : ControllerBaseIscasLune
{
    private readonly ITamanhoProdutoService _service;

    public TamanhoProdutoController(ITamanhoProdutoService service)
    {
        _service = service;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateTamanhoProduto(CreateTamanhoProdutoDto createTamanhoProdutoDto)
    {
        try
        {
            var result = await _service.CreateTamanhoProdutoAsync(createTamanhoProdutoDto);
            if (!result) return BadRequest("Ocorreu um erro interno");
            return Ok();
        }
        catch (Exception ex)
        {
            return HandleError(ex.Message);
        }
    }
}
