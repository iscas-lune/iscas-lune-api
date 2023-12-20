using iscas_lune_api.Application.Interfaces;
using iscas_lune_api.Dtos.Usuarios;
using iscaslune.Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace iscas_lune_api.Controllers;

[ApiController]
[Route("api/usuario")]
public class UsuarioController : ControllerBaseIscasLune
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [EnableCors("iscasluneorigin")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpGet("get-conta")]
    public async Task<IActionResult> GetConta()
    {
        try
        {
            var usuario = await _usuarioService.GetConta();
            return HandleGet(usuario);
        }
        catch (Exception ex)
        {
            return HandleError(ex.Message);
        }
    }

    [EnableCors("iscasluneoriginwithpost")]
    [HttpPost("create")]
    public async Task<IActionResult> CreateUsuario(CreateUsuarioDto createUsuarioDto)
    {
        try
        {
            var result = await _usuarioService.CreateUsuarioAsync(createUsuarioDto);
            if (!string.IsNullOrWhiteSpace(result?.Error))
                return BadRequest(new { message = result?.Error });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return HandleError(ex.Message);
        }
    }

    [EnableCors("iscasluneoriginwithpost")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateUsuario(UpdateUsuarioDto updateUsuarioDto)
    {
        try
        {
            var result = await _usuarioService.UpdateUsuarioAsync(updateUsuarioDto);
            if (!string.IsNullOrWhiteSpace(result?.Error))
                return BadRequest(new { message = result?.Error });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return HandleError(ex.Message);
        }
    }

    [EnableCors("iscasluneoriginwithpost")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPut("update-senha")]
    public async Task<IActionResult> UpdateSenhaUsuario(UpdateSenhaUsuarioDto updateUsuarioDto)
    {
        try
        {
            var result = await _usuarioService.UpdateSenhaAsync(updateUsuarioDto);
            if (!result) return BadRequest(new { message = "Ocorreu um erro interno, tente novamente mais tarde!" });

            return Ok(new { message = "Senha alterada com sucesso!" });
        }
        catch (Exception ex)
        {
            return HandleError(ex.Message);
        }
    }
}
