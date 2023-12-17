using iscaslune.Api;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace iscas_lune_api.CrossCutting;

public static class DependencyInjectJwt
{
    public static void InjectJwt(this IServiceCollection services)
    {
        services.AddAuthentication(
            JwtBearerDefaults.AuthenticationScheme).
            AddJwtBearer(options =>
             options.TokenValidationParameters = OptionsToken());
    }

    private static TokenValidationParameters OptionsToken()
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidAudience = EnvironmentVariable.GetVariable("JWT_AUDIENCE"),
            ValidIssuer = EnvironmentVariable.GetVariable("JWT_ISSUER"),
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                     Encoding.UTF8.GetBytes(EnvironmentVariable.GetVariable("JWT_KEY")))
        };
    }
}
