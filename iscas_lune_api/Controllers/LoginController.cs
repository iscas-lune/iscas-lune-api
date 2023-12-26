using iscas_lune_api.Application.Interfaces;
using iscas_lune_api.Discord.Client;
using iscas_lune_api.Model.Login;
using iscaslune.Api.Controllers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace iscas_lune_api.Controllers;

[ApiController]
[EnableCors("iscasluneoriginwithpost")]
[Route("api/usuario")]
public class LoginController : ControllerBaseIscasLune
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService, IDiscordNotification discordNotification) : base(discordNotification)
    {
        _loginService = loginService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(RequestLoginModel requestLoginModel)
    {
        try
        {
            var result = await _loginService.LoginAsync(requestLoginModel);
            if(!string.IsNullOrWhiteSpace(result.Error))
                return BadRequest(new { message = result.Error });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return await HandleError(ex.Message);
        }
    }
}
