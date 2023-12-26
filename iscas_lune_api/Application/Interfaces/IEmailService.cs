namespace iscas_lune_api.Application.Interfaces;

public interface IEmailService
{
    bool SendEmail(string email, string message, string assunto,byte[]? arquivo = null, string? nomeArquivo = null, string? tipoArquivo = null);
}
