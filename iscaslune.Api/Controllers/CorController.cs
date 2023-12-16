using iscaslune.Api.Application.Interfaces;
using iscaslune.Api.Dtos.Cores;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace iscaslune.Api.Controllers;

[Route("api/cor")]
public class CorController(ICorService corService) : ControllerBaseIscasLune
{
    private readonly ICorService _corService = corService;

    [HttpPost("create")]
    public async Task<IActionResult> CreateCor(CreateCorDto createCorDto)
    {
        try
        {
            var result = await _corService.CreateCorAsync(createCorDto);
            return HandleCreated(result, $"api/cor/get?id={result?.Id}");
        }
        catch (Exception ex)
        {
            return HandleError(ex.Message);
        }
    }
    [EnableCors("iscasluneorigin")]
    [HttpGet("get")]
    public async Task<IActionResult> GetCor([FromQuery] Guid id)
    {
        try
        {
            var corViewmodel = await _corService.GetCorByIdAsync(id);
            return HandleGet(corViewmodel);
        }
        catch (Exception ex)
        {
            return HandleError(ex.Message);
        }
    }
    [EnableCors("iscasluneorigin")]
    [HttpGet("list")]
    public async Task<IActionResult> ListCores([FromQuery] PaginacaoCorDto paginacaoCorDto)
    {
        try
        {
            var cores = await _corService.GetCoresAsync(paginacaoCorDto);
            return HandleGet(cores);
        }
        catch (Exception ex)
        {
            return HandleError(ex.Message);
        }
    }
}
