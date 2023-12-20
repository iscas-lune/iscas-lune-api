namespace iscas_lune_api.Application.Interfaces;

public interface IEmailService
{
    bool SendEmail(string email, string message);
}
