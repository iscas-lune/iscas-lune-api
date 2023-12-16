using iscaslune.Api.Application.Interfaces;
using iscaslune.Api.Application.Services;
using iscaslune.Api.Dtos.Categorias;
using iscaslune.Api.Dtos.Produtos;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace iscaslune.Api.Controllers;

[ApiController]
[Route("api/produto")]
public class ProdutoController 
    : ControllerBaseIscasLune
{
    private readonly IProdutoService _produtoService;

    public ProdutoController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateProduto(CreateProdutoDto createProdutoDto)
    {
        try
        {
            var result = await _produtoService.CreateProdutoAsync(createProdutoDto);
            return HandleCreated(result, $"api/produto/get?id={result?.Id}");
        }
        catch (Exception ex)
        {
            return HandleError(ex.Message);
        }
    }

    [EnableCors("iscasluneorigin")]
    [HttpGet("get")]
    public async Task<IActionResult> GetProduto([FromQuery] Guid id)
    {
        try
        {
            var result = await _produtoService.GetProdutoByIdAsync(id);
            return HandleGet(result);
        }
        catch (Exception ex)
        {
            return HandleError(ex.Message);
        }
    }

    [HttpGet("list")]
    [EnableCors("iscasluneorigin")]
    public async Task<IActionResult> ListProdutos([FromQuery] PaginacaoProdutoDto paginacaoProdutoDto)
    {
        try
        {
            var result = await _produtoService.GetProdutosAsync(paginacaoProdutoDto);
            return HandleGet(result);
        }
        catch (Exception ex)
        {
            return HandleError(ex.Message);
        }
    }

    [HttpGet("list-by-categorias")]
    [EnableCors("iscasluneorigin")]
    public async Task<IActionResult> ListProdutosByCategorias([FromQuery] Guid categoriaId)
    {
        try
        {
            var result = await _produtoService.GetProdutosByCategoriaAsync(categoriaId);
            return HandleGet(result);
        }
        catch (Exception ex)
        {
            return HandleError(ex.Message);
        }
    }
}
