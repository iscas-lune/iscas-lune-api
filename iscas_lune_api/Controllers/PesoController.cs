using iscas_lune_api.Discord.Client;
using iscaslune.Api.Application.Interfaces;
using iscaslune.Api.Dtos.Cores;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace iscaslune.Api.Controllers;

[Route("api/peso")]
public class PesoController : ControllerBaseIscasLune
{
    private readonly IPesoService _pesoService;

    public PesoController(IDiscordNotification discordNotification, IPesoService pesoService) : base(discordNotification)
    {
        _pesoService = pesoService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateCor(CreatePesoDto createCorDto)
    {
        try
        {
            var result = await _pesoService.CreateCorAsync(createCorDto);
            return HandleCreated(result, $"api/cor/get?id={result?.Id}");
        }
        catch (Exception ex)
        {
            return await HandleError(ex.Message);
        }
    }
    [EnableCors("iscasluneorigin")]
    [HttpGet("get")]
    public async Task<IActionResult> GetCor([FromQuery] Guid id)
    {
        try
        {
            var corViewmodel = await _pesoService.GetCorByIdAsync(id);
            return HandleGet(corViewmodel);
        }
        catch (Exception ex)
        {
            return await HandleError(ex.Message);
        }
    }
    [EnableCors("iscasluneorigin")]
    [HttpGet("list")]
    public async Task<IActionResult> ListCores([FromQuery] PaginacaoPesoDto paginacaoCorDto)
    {
        try
        {
            var cores = await _pesoService.GetCoresAsync(paginacaoCorDto);
            return HandleGet(cores);
        }
        catch (Exception ex)
        {
            return await HandleError(ex.Message);
        }
    }
}
