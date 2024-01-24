using iscas_lune_api.Discord.Client;
using iscas_lune_api.Dtos.Produtos;
using iscaslune.Api.Application.Interfaces;
using iscaslune.Api.Application.Services;
using iscaslune.Api.Domain.Context;
using iscaslune.Api.Dtos.Categorias;
using iscaslune.Api.Dtos.Produtos;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text;

namespace iscaslune.Api.Controllers;

[ApiController]
[Route("api/produto")]
public class ProdutoController 
    : ControllerBaseIscasLune
{
    private readonly IProdutoService _produtoService;
    private readonly IscasLuneContext _context;

    public ProdutoController(IProdutoService produtoService, IDiscordNotification discordNotification, IscasLuneContext context) : base(discordNotification)
    {
        _produtoService = produtoService;
        _context = context;
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
            return await HandleError(ex.Message);
        }
    }

    [HttpPut("edit")]
    public async Task<IActionResult> EditFotoProduto(EditFotoProduto editFotoProduto)
    {
        var produto = await _context.Produtos.FirstOrDefaultAsync(x => x.Id == editFotoProduto.Id);

        if (produto is null) return BadRequest();

        produto.Foto = Encoding.UTF8.GetBytes(editFotoProduto.Foto);

        _context.Update(produto);
        await _context.SaveChangesAsync();
        return Ok();
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
            return await HandleError(ex.Message);
        }
    }

    [HttpGet("list")]
    [EnableCors("iscasluneorigin")]
    public async Task<IActionResult> ListProdutos([FromQuery] int page)
    {
        try
        {
            var stop = new Stopwatch();
            stop.Start();
            var result = await _produtoService.GetProdutosAsync(page);
            stop.Stop();
            Console.WriteLine($"Tempo : {stop.ElapsedMilliseconds}");
            
            return HandleGet(result);
        }
        catch (Exception ex)
        {
            return await HandleError(ex.Message);
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
            return await HandleError(ex.Message);
        }
    }
}
