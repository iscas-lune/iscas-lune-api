using iscas_lune_api.Application.Interfaces;
using iscas_lune_api.Discord.Client;
using iscaslune.Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace iscas_lune_api.Controllers;

[ApiController]
[Route("api/itens-pedido")]
public class ItensPedidoController : ControllerBaseIscasLune
{
    private readonly IItensPedidoService _itensPedidoService;

    public ItensPedidoController(
        IDiscordNotification discordNotification, 
        IItensPedidoService itensPedidoService) 
            : base(discordNotification)
    {
        _itensPedidoService = itensPedidoService;
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    [EnableCors("iscasluneorigin")]
    [HttpGet("get-pedido-id")]
    public async Task<IActionResult> GetByPedido([FromQuery] Guid pedidoId)
    {
        try
        {
            var itens = await _itensPedidoService.GetItensByPedidoIdAsync(pedidoId);
            return Ok(itens);
        }
        catch (Exception ex)
        {
            return await HandleError(ex.Message);
        }
    }
}
