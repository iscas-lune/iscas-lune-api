﻿using iscas_lune_api.Infrastructure.Cached;
using iscas_lune_api.Infrastructure.Interfaces;
using iscas_lune_api.Infrastructure.Repositories;
using iscaslune.Api.Infrastructure.Cached;
using iscaslune.Api.Infrastructure.Interfaces;
using iscaslune.Api.Infrastructure.Repositories;

namespace iscaslune.Api.CrossCutting;

public static class DependencyInjectInfrastructure
{
    public static void InjectInfrastructure(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<CorRepository>();
        services.AddScoped<ICorRepository, CorCached>();

        services.AddScoped<TamanhoRepository>();
        services.AddScoped<ITamanhoRepository, TamanhoCached>();

        services.AddScoped<CategoriaRepository>();
        services.AddScoped<ICategoriaRepository, CategoriaCached>();

        services.AddScoped<ProdutoRepository>();
        services.AddScoped<IProdutoRepository, ProdutoCached>();

        services.AddScoped<BannerRepository>();
        services.AddScoped<IBannerRepository, BannerCached>();
        services.AddScoped<ICorProdutoRepository, CorProdutoRepository>();
        services.AddScoped<ITamanhoProdutoRepository, TamanhoProdutoRepository>();
    }
}