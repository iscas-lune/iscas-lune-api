using iscas_lune_api.Application.Interfaces;
using iscas_lune_api.Discord.Client;
using iscas_lune_api.Dtos.Cores;
using iscaslune.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace iscas_lune_api.Controllers;

[ApiController]
[Route("api/cor-produto")]
public class CorProdutoController : ControllerBaseIscasLune
{
    private readonly ICorProdutoService _corProdutoService;

    public CorProdutoController(ICorProdutoService corProdutoService, IDiscordNotification discordNotification) : base(discordNotification)
    {
        _corProdutoService = corProdutoService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateCorProduto(CreateCorProdutoDto corProdutoDto)
    {
        try
        {
            var result = await _corProdutoService.CreateCorProdutoAsync(corProdutoDto);
            if (!result) return BadRequest("Ocorreu um erro interno");
            return Ok();
        }
        catch (Exception ex)
        {
            return await HandleError(ex.Message);
        }
    }
}
