using iscaslune.Api.Errors;
using Microsoft.AspNetCore.Mvc;

namespace iscaslune.Api.Controllers;

[ApiController]
public abstract class ControllerBaseIscasLune : ControllerBase
{
    private readonly string _ambiente = EnvironmentVariable.GetVariable("AMBIENTE");
    private readonly ErrorResponseGeneric _error = new();
    protected IActionResult HandleError(string message)
    {
        if (ValidAmbiente(message))
            return BadRequest(message);
        return BadRequest(_error);
    }

    protected IActionResult HandleCreated(object? obj, string path)
    {
        if (obj == null) return BadRequest(_error);
        return Created(path, obj);    
    }

    protected IActionResult HandleGet(object? model)
    {
        if (model == null)
        {
            var message = "Registro não encontrado!";
            if (ValidAmbiente(message))
                return BadRequest(message);

            return BadRequest(_error);
        }

        return Ok(model);
    }

    private bool ValidAmbiente(string message)
    {
        return _ambiente.Equals("develop") && !string.IsNullOrWhiteSpace(message);
    }
}
