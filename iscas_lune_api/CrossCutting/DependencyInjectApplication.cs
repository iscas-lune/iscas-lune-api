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
    }
}
