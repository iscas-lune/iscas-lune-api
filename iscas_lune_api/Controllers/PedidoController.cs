using iscas_lune_api.Application.Interfaces;
using iscas_lune_api.Discord.Client;
using iscas_lune_api.Dtos.Pedidos;
using iscaslune.Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace iscas_lune_api.Controllers;

[ApiController]
[Route("api/pedido")]

public class PedidoController : ControllerBaseIscasLune
{
    private readonly IPedidoService _pedidoService;

    public PedidoController(IPedidoService pedidoService, IDiscordNotification discordNotification) : base(discordNotification)
    {
        _pedidoService = pedidoService;
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    [EnableCors("iscasluneoriginwithpost")]
    [HttpPost("create")]
    public async Task<IActionResult> CreatePedido(PedidoCreateDto pedidoCreateDto)
    {
        try
        {
            var result = await _pedidoService.CreatePedidoAsync(pedidoCreateDto);
            if (!string.IsNullOrWhiteSpace(result.error))
                return BadRequest(new { message = result.error });
            return Ok(new { message = "Pedido criado com sucesso!" });
        }
        catch (Exception ex)
        {
            return await HandleError(ex.Message);
        }
    }

    [HttpPut("update-status")]
    public async Task<IActionResult> UpdateStatusPedido(UpdateStatusPedidoDto updateStatusPedidoDto)
    {
        try
        {
            var result = await _pedidoService.UpdateStatusPedidoAsync(updateStatusPedidoDto);
            if (!result) return await HandleError("Não foi possível atualizar o status do pedido!");
            return Ok(new { message = "Pedido atualizado com sucesso!" });
        }
        catch (Exception ex)
        {
            return await HandleError(ex.Message);
        }
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    [EnableCors("iscasluneorigin")]
    [HttpGet("list")]
    public async Task<IActionResult> GetPedidos([FromQuery] int statusPedido)
    {
        try
        {
            var pedidos = await _pedidoService.GetPedidosUsuario(statusPedido);
            return Ok(pedidos);
        }
        catch (Exception ex)
        {
            return await HandleError(ex.Message);
        }
    }
}
