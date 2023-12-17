using iscas_lune_api.Application.Interfaces;
using iscas_lune_api.Model;

namespace iscas_lune_api.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly ITokenService _tokenService;

    public UsuarioService(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public UsuarioViewModel? GetConta()
    {
        return _tokenService.GetClaims();
    }
}
