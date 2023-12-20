using iscas_lune_api.Application.Interfaces;
using iscas_lune_api.Infrastructure.Interfaces;
using iscas_lune_api.Model.Login;
using iscas_lune_api.Model.Usuarios;
using static BCrypt.Net.BCrypt;

namespace iscas_lune_api.Application.Services;

public class LoginService : ILoginService
{
    private readonly ILoginRepository _loginRepository;
    private readonly ITokenService _tokenService;

    public LoginService(ILoginRepository loginRepository, ITokenService tokenService)
    {
        _loginRepository = loginRepository;
        _tokenService = tokenService;
    }

    public async Task<ResponseLoginModel> LoginAsync(RequestLoginModel requestLoginModel)
    {
        var usuario = await _loginRepository.LoginAsync(requestLoginModel.Email);
        if (usuario == null || !Verify(requestLoginModel.Senha, usuario.Senha))
            return new(new(), "", "E-mail ou senha inválidos!");

        return new ResponseLoginModel(
            new UsuarioViewModel().ForModel(usuario) ?? new()
            , _tokenService.GenerateToken(usuario),
            null);
    }
}
