using iscas_lune_api.Application.Interfaces;
using iscaslune.Api.Controllers;
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

    public CarrinhoController(ICarrinhoService carrinhoService)
    {
        _carrinhoService = carrinhoService;
    }

    [EnableCors("iscasluneoriginwithpost")]
    [HttpPut("adicionar")]
    public async Task<IActionResult> AdicionarCarinho([FromQuery] Guid produtoId)
    {
        try
        {
            var result = await _carrinhoService.AdicionarProdutoAsync(produtoId);
            if (!result) return BadRequest(new { message = "Ocorreu um erro interno, tente novamente mais tarde!" });
            return Ok(new { message = "Produto adicionado com sucesso!" });
        }
        catch (Exception ex)
        {
            return HandleError(ex.Message);
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
        catch (Exception ex)
        {
            return HandleError(ex.Message);
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
        catch (Exception ex)
        {
            return HandleError(ex.Message);
        }
    }
}
