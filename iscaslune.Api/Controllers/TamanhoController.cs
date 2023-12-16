using iscaslune.Api.Application.Interfaces;
using iscaslune.Api.Dtos.Tamanhos;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace iscaslune.Api.Controllers;

[ApiController]
[Route("api/tamanho")]
public class TamanhoController(ITamanhoService tamanhoService)
    : ControllerBaseIscasLune
{
    private readonly ITamanhoService _tamanhoService = tamanhoService;

    [HttpPost("create")]
    public async Task<IActionResult> CreateTamanho(CreateTamanhoDto createTamanhoDto)
    {
        try
        {
            var result = await _tamanhoService.CreateTamanhoAsync(createTamanhoDto);
            return HandleCreated(result, $"/api/get?id={result?.Id}");
        }
        catch (Exception ex)
        {
            return HandleError(ex.Message);
        }
    }

    [EnableCors("iscasluneorigin")]
    [HttpGet("get")]
    public async Task<IActionResult> GetTamanho([FromQuery] Guid id)
    {
        try
        {
            var tamanho = await _tamanhoService.GetTamanhobyIdAsync(id);
            return HandleGet(tamanho);
        }
        catch (Exception ex)
        {
            return HandleError(ex.Message);
        }
    }

    [EnableCors("iscasluneorigin")]
    [HttpGet("list")]
    public async Task<IActionResult> ListTamanhos([FromQuery] PaginacaoTamanhoDto paginacaoTamanhoDto)
    {
        try
        {
            var tamanhos = await _tamanhoService.GetTamanhosAsync(paginacaoTamanhoDto);
            return HandleGet(tamanhos);
        }
        catch (Exception ex)
        {
            return HandleError(ex.Message);
        }
    }
}
