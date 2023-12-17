using iscas_lune_api.Application.Interfaces;
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
    public IActionResult GetConta()
    {
        try
        {
            var usuario = _usuarioService.GetConta();
            return HandleGet(usuario);
        }
        catch (Exception ex)
        {
            return HandleError(ex.Message);
        }
    }
}
