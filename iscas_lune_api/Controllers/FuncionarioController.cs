using iscas_lune_api.Application.Interfaces;
using iscas_lune_api.Discord.Client;
using iscas_lune_api.Exceptions;
using iscas_lune_api.Model.Login;
using iscaslune.Api.Controllers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace iscas_lune_api.Controllers;

[ApiController]
[Route("api/funcionario")]
public class FuncionarioController : ControllerBaseIscasLune
{
    private readonly IFuncionarioService _funcionarioService;
    public FuncionarioController(
        IDiscordNotification discordNotification,
        IFuncionarioService funcionarioService) : base(discordNotification)
    {
        _funcionarioService = funcionarioService;
    }

    [HttpPost("login")]
    [EnableCors("iscasluneoriginadmin")]
    public async Task<IActionResult> LoginAsync(RequestLoginModel requestLoginModel)
    {
        try
        {
            var response = await _funcionarioService.LoginFuncionarioAsync(requestLoginModel);

            var data = new { Token = response.token, UserData = response.funcionario };
            return Ok(data);
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
