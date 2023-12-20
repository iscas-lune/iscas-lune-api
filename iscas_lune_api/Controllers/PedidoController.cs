using iscas_lune_api.Application.Interfaces;
using iscas_lune_api.Dtos.Pedidos;
using iscaslune.Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace iscas_lune_api.Controllers;

[ApiController]
[Route("api/pedido")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class PedidoController : ControllerBaseIscasLune
{
    private readonly IPedidoService _pedidoService;

    public PedidoController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

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
            return HandleError(ex.Message);
        }
    }

    [EnableCors("iscasluneorigin")]
    [HttpGet("list")]
    public async Task<IActionResult> GetPedidos()
    {
        try
        {
            var pedidos = await _pedidoService.GetPedidosUsuario();
            return Ok(pedidos);
        }
        catch (Exception ex)
        {
            return HandleError(ex.Message);
        }
    }
}
