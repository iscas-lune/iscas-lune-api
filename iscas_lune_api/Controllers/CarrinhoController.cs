using iscas_lune_api.Application.Interfaces;
using iscas_lune_api.Discord.Client;
using iscas_lune_api.Dtos.Carrinhos;
using iscas_lune_api.Exceptions;
using iscaslune.Api.Controllers;
using iscaslune.Api.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace iscas_lune_api.Controllers;

[ApiController]
[Route("api/carrinho")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class CarrinhoController : ControllerBaseIscasLune
{
    private readonly ICarrinhoService _carrinhoService;

    public CarrinhoController(ICarrinhoService carrinhoService, IDiscordNotification discordNotification) : base(discordNotification)
    {
        _carrinhoService = carrinhoService;
    }

    [EnableCors("iscasluneoriginwithpost")]
    [HttpPut("adicionar")]
    public async Task<IActionResult> AdicionarCarinho(AddCarrinhoDto addCarrinhoDto)
    {
        try
        {
            var result = await _carrinhoService.AdicionarProdutoAsync(addCarrinhoDto);
            if (!result) return BadRequest(new { message = "Ocorreu um erro interno, tente novamente mais tarde!" });
            return Ok(new { message = "Produto adicionado com sucesso!" });
        }
        catch (ExceptionApi ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return await HandleError(ex.Message);
        }
    }

    [EnableCors("iscasluneorigin")]
    [HttpGet("get-carrinho")]
    public async Task<IActionResult> GetCarrinho()
    {
        try
        {
            var result = await _carrinhoService.GetCarrinhoAsync();
            return Ok(result);
        }
        catch (ExceptionApi ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return await HandleError(ex.Message);
        }
    }

    [EnableCors("iscasluneorigin")]
    [HttpGet("get-carrinho-count")]
    public async Task<IActionResult> GetCarrinhoCount()
    {
        try
        {
            var result = await _carrinhoService.GetCountCarrinhoAsync();
            return Ok(result);
        }
        catch (ExceptionApi ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return await HandleError(ex.Message);
        }
    }

    [EnableCors("iscasluneoriginwithdelte")]
    [HttpDelete("delete-produto-carrinho")]
    public async Task<IActionResult> DeleteProdutCarrinho([FromQuery] Guid produtoId)
    {
        try
        {
            var result = await _carrinhoService.DeleteProdutoCarrinhoAsync(produtoId);
            if (!result) return BadRequest(new { message = ErrorResponseGeneric.Error });
            return Ok();
        }
        catch (ExceptionApi ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return await HandleError(ex.Message);
        }
    }
}
