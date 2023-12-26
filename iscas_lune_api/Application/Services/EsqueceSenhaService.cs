using iscas_lune_api.Application.Interfaces;
using iscas_lune_api.Dtos.Usuarios;
using iscas_lune_api.Infrastructure.Interfaces;
using System;

namespace iscas_lune_api.Application.Services;

public class EsqueceSenhaService : IEsqueceSenhaService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IEmailService _emailService;

    public EsqueceSenhaService(IUsuarioRepository usuarioRepository, IEmailService emailService)
    {
        _usuarioRepository = usuarioRepository;
        _emailService = emailService;
    }

    public async Task<bool> RecuperarSenhaAsync(EsqueceuSenhaDto esqueceuSenhaDto)
    {
        var usuario = await _usuarioRepository.GetUsuarioByEmailAsync(esqueceuSenhaDto.Email);
        if (usuario == null) return false;
        var senha = new Random().Next(80000, 999999).ToString();
        usuario.UpdateSenha(senha);
        var result = await _usuarioRepository.UpdateAsync(usuario);
        if (!result) return false;
        var message = $"Recuperação de senha efetuada com sucesso!\nSua nova senha é {senha} .\n Importante!\nNo Próximo acesso ao nosso site, efetue a troca da senha.\nAtt: Iscas lune.";
        var assunto = "Recuperação de senha";
        return _emailService.SendEmail(usuario.Email, message, assunto);
    }
}
