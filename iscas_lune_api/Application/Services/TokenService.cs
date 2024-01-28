using iscas_lune_api.Application.Interfaces;
using iscas_lune_api.Domain.Entities;
using iscas_lune_api.Exceptions;
using iscas_lune_api.Model.Usuarios;
using iscaslune.Api;
using iscaslune.Api.Migrations;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace iscas_lune_api.Application.Services;

public class TokenService : ITokenService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string _key;
    private readonly string _issuer;
    private readonly string _audience;

    public TokenService(IHttpContextAccessor httpContextAccessor)
    {
        _key = EnvironmentVariable.GetVariable("JWT_KEY");
        _issuer = EnvironmentVariable.GetVariable("JWT_ISSUER");
        _audience = EnvironmentVariable.GetVariable("JWT_AUDIENCE");
        _httpContextAccessor = httpContextAccessor;
    }
    public string GenerateToken(Usuario usuario)
    {
        var claims = new[]
        {
            new Claim("Nome", usuario.Nome),
            new Claim("Email", usuario.Email),
            new Claim("Numero", usuario.Numero.ToString()),
            new Claim("Id", usuario.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_key));

        var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiration = DateTime.UtcNow.AddHours(24);

        var token = new JwtSecurityToken(
          issuer: _issuer,
          audience: _audience,
          claims: claims,
          expires: expiration,
          signingCredentials: credenciais);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenString;
    }

    public string GenerateTokenFuncionario(Funcionario funcionario)
    {
        var claims = new[]
        {
            new Claim("Nome", funcionario.Nome),
            new Claim("Email", funcionario.Email),
            new Claim("Numero", funcionario.Numero.ToString()),
            new Claim("Id", funcionario.Id.ToString()),
            new Claim("IsFuncionario", "TRUE"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_key));

        var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiration = DateTime.UtcNow.AddHours(24);

        var token = new JwtSecurityToken(
          issuer: _issuer,
          audience: _audience,
          claims: claims,
          expires: expiration,
          signingCredentials: credenciais);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenString;
    }


    public UsuarioViewModel GetClaims()
    {
        if (_httpContextAccessor?.HttpContext?.User.Identity is not ClaimsIdentity claimsIdentity 
            || !claimsIdentity.Claims.Any())
            throw new ExceptionApi(nameof(claimsIdentity));

        var id = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "Id")?.Value
            ?? throw new ExceptionApi("token inválido");
        var numero = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "Numero")?.Value
            ?? throw new ExceptionApi("token inválido");
        var nome = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "Nome")?.Value
            ?? throw new ExceptionApi("token inválido");
        var email = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "Email")?.Value
            ?? throw new ExceptionApi("token inválido");

        return new UsuarioViewModel()
        {
            Id = Guid.Parse(id),
            Nome = nome,
            Email = email,
            Numero = long.Parse(numero)
        };
    }

    public bool IsFuncionario()
    {
        if (_httpContextAccessor?.HttpContext?.User.Identity is not ClaimsIdentity claimsIdentity
            || !claimsIdentity.Claims.Any())
            throw new ExceptionApi(nameof(claimsIdentity));

        var isFuncionario = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "IsFuncionario")?.Value;

        return !string.IsNullOrWhiteSpace(isFuncionario);
    }
}
