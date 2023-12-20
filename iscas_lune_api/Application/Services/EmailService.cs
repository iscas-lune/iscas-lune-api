using iscas_lune_api.Application.Interfaces;
using iscaslune.Api;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using Microsoft.IdentityModel.Tokens;

namespace iscas_lune_api.Application.Services;

public class EmailService : IEmailService
{
    private readonly string _from;
    private readonly string _server;
    private readonly string _password;
    private readonly int _port;

    public EmailService()
    {
        _from = EnvironmentVariable.GetVariable("EMAIL");
        _server = EnvironmentVariable.GetVariable("EMAIL_SERVER");
        _password = EnvironmentVariable.GetVariable("EMAIL_PASSWORD");
        _port = int.Parse(EnvironmentVariable.GetVariable("EMAIL_PORT"));
    }

    public bool SendEmail(string email, string message)
    {
        try
        {
            var mail = new MailMessage(_from, email)
            {
                Subject = "Recuperação de senha",
                SubjectEncoding = System.Text.Encoding.GetEncoding("UTF-8"),
                BodyEncoding = System.Text.Encoding.GetEncoding("UTF-8"),
                Body = message
            };

            var smtp = new SmtpClient(_server, _port);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_from, _password);
            smtp.Send(mail);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}
