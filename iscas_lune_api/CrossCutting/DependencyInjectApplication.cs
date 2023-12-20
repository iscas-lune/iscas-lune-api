using iscas_lune_api.Application.Interfaces;
using iscas_lune_api.Application.Services;
using iscaslune.Api.Application.Interfaces;
using iscaslune.Api.Application.Services;

namespace iscaslune.Api.CrossCutting;

public static class DependencyInjectApplication
{
    public static void InjectApplication(this IServiceCollection services)
    {
        var connectionString = EnvironmentVariable.GetVariable("REDIS_URL");
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = connectionString;
        });

        services.AddScoped(typeof(ICachedService<>), typeof(CachedService<>));
        services.AddScoped<ICorService, CorService>();
        services.AddScoped<ITamanhoService, TamanhoService>();
        services.AddScoped<ICategoriaService, CategoriaService>();
        services.AddScoped<IBannerService, BannerService>();
        services.AddScoped<IProdutoService, ProdutoService>();
        services.AddScoped<ICorProdutoService, CorProdutoService>();
        services.AddScoped<ITamanhoProdutoService, TamanhoProdutoService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<ILoginService, LoginService>();
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IEsqueceSenhaService, EsqueceSenhaService>();
        services.AddTransient<ICarrinhoService, CarrinhoService>();
    }
}
