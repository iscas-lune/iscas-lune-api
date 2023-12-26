using iscas_lune_api.Application.Interfaces;
using iscas_lune_api.Discord.Client;
using iscas_lune_api.Dtos.Usuarios;
using iscaslune.Api.Controllers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace iscas_lune_api.Controllers;

[ApiController]
[Route("api/usuario")]
[EnableCors("iscasluneoriginwithpost")]
public class EsqueceuSenhaController : ControllerBaseIscasLune
{
    private readonly IEsqueceSenhaService _esqueceSenhaService;

    public EsqueceuSenhaController(IEsqueceSenhaService esqueceSenhaService, IDiscordNotification discordNotification) : base(discordNotification)
    {
        _esqueceSenhaService = esqueceSenhaService;
    }

    [HttpPut("esqueceu-senha")]
    public async Task<IActionResult> EsqueceuSenha(EsqueceuSenhaDto esqueceuSenhaDto)
    {
        try
        {
            var result = await _esqueceSenhaService.RecuperarSenhaAsync(esqueceuSenhaDto);
            if (!result)
                return BadRequest(new { message = "Ocorreu um erro interno, tente novamente mais tarde!" });
            return Ok(new { message = "Acesse seu e-mail para acessar sua nova senha!" });
        }
        catch (Exception ex)
        {
            return await HandleError(ex.Message);
        }
    }
}
