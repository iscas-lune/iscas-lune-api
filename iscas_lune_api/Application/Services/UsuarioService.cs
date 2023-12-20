using iscas_lune_api.Application.Interfaces;
using iscas_lune_api.Domain.Entities;
using iscas_lune_api.Dtos.Usuarios;
using iscas_lune_api.Infrastructure.Interfaces;
using iscas_lune_api.Model.Login;
using iscas_lune_api.Model.Usuarios;
using iscaslune.Api.Application.Interfaces;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace iscas_lune_api.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly ITokenService _tokenService;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ICachedService<Usuario> _cachedService;

    public UsuarioService(ITokenService tokenService, IUsuarioRepository usuarioRepository, ICachedService<Usuario> cachedService)
    {
        _tokenService = tokenService;
        _usuarioRepository = usuarioRepository;
        _cachedService = cachedService;
    }

    public async Task<ResponseLoginModel?> CreateUsuarioAsync(CreateUsuarioDto createUsuarioDto)
    {
        var usuario = await _usuarioRepository.GetUsuarioByEmailAsync(createUsuarioDto.Email);

        if (usuario != null) return new(new(), "", "Este e-mail já se encontra cadastrado!");

        usuario = createUsuarioDto.ForEntity();
        var result = await _usuarioRepository.AddAsync(usuario);
        if (!result) return null;
        var usuarioViewModel = new UsuarioViewModel().ForModel(usuario)
            ?? throw new ArgumentException("Usuário inválido!");
        return new ResponseLoginModel(usuarioViewModel, _tokenService.GenerateToken(usuario), null);
    }

    public async Task<UsuarioViewModel?> GetConta()
    {
        var claims = _tokenService.GetClaims() ?? 
            throw new Exception("Usuário não localizado!");

        var usuario = await _usuarioRepository.GetUsuarioByIdAsync(claims.Id);
        return new UsuarioViewModel().ForModel(usuario);
    }

    public async Task<bool> UpdateSenhaAsync(UpdateSenhaUsuarioDto updateSenhaUsuarioDto)
    {
        var claims = _tokenService.GetClaims() ?? new();
        var usuario = await _usuarioRepository.GetUsuarioByIdAsync(claims.Id);
        if (usuario == null ||
            !updateSenhaUsuarioDto.Senha.Equals(updateSenhaUsuarioDto.ReSenha)) return false;
        usuario.UpdateSenha(updateSenhaUsuarioDto.Senha);
        await _cachedService.RemoveCachedAsync(usuario.Id.ToString());
        return await _usuarioRepository.UpdateAsync(usuario);
    }

    public async Task<ResponseLoginModel?> UpdateUsuarioAsync(UpdateUsuarioDto updateUsuarioDto)
    {
        var claims = _tokenService.GetClaims() ?? new();
        var usuario = await _usuarioRepository.GetUsuarioByIdAsync(claims.Id)
            ?? throw new Exception("Ocorreu um erro interno, tente novamente mais tarde!");
        usuario.Update(updateUsuarioDto.Email, updateUsuarioDto.Nome, updateUsuarioDto.Telefone);
        var result = await _usuarioRepository.UpdateAsync(usuario);
        if (!result) throw new Exception("Ocorreu um erro interno, tente novamente mais tarde!");
        var usuarioViewModel = new UsuarioViewModel().ForModel(usuario)
            ?? throw new ArgumentException("Usuário inválido!");
        await _cachedService.RemoveCachedAsync(usuario.Id.ToString());
        return new ResponseLoginModel(usuarioViewModel, _tokenService.GenerateToken(usuario), null);
    }
}
