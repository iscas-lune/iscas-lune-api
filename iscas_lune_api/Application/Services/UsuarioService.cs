using iscas_lune_api.Application.Interfaces;
using iscas_lune_api.Dtos.Usuarios;
using iscas_lune_api.Infrastructure.Interfaces;
using iscas_lune_api.Model.Usuarios;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace iscas_lune_api.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly ITokenService _tokenService;
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(ITokenService tokenService, IUsuarioRepository usuarioRepository)
    {
        _tokenService = tokenService;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<UsuarioCreateViewModel?> CreateUsuarioAsync(CreateUsuarioDto createUsuarioDto)
    {
        var usuario = await _usuarioRepository.GetUsuarioByEmailAsync(createUsuarioDto.Email);

        if (usuario != null) return new(new(), "", "Este e-mail já se encontra cadastrado!");

        usuario = createUsuarioDto.ForEntity();
        var result = await _usuarioRepository.AddAsync(usuario);
        if (!result) return null;
        var usuarioViewModel = new UsuarioViewModel().ForModel(usuario)
            ?? throw new ArgumentException("Usuário inválido!");
        return new UsuarioCreateViewModel(usuarioViewModel, _tokenService.GenerateToken(usuario), null);
    }

    public UsuarioViewModel? GetConta()
    {
        return _tokenService.GetClaims();
    }
}
