using iscas_lune_api.Infrastructure.Cached;
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
        services.AddScoped<PesoRepository>();
        services.AddScoped<IPesoRepository, PesoCached>();

        services.AddScoped<TamanhoRepository>();
        services.AddScoped<ITamanhoRepository, TamanhoCached>();

        services.AddScoped<CategoriaRepository>();
        services.AddScoped<ICategoriaRepository, CategoriaCached>();

        services.AddScoped<ProdutoRepository>();
        services.AddScoped<IProdutoRepository, ProdutoCached>();

        services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();

        services.AddScoped<UsuarioRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioCached>();

        services.AddScoped<BannerRepository>();
        services.AddScoped<IBannerRepository, BannerCached>();
        services.AddScoped<ICorProdutoRepository, CorProdutoRepository>();

        services.AddScoped<TamanhoProdutoRepository>();
        services.AddScoped<ITamanhoProdutoRepository, TamanhosProdutosCached>();
        services.AddScoped<ILoginRepository, LoginRepository>();

        services.AddScoped<IPedidoRepository, PedidoRepository>();
        services.AddScoped<IPedidosEmAbertoRepository, PedidosEmAbertoRepository>();

        services.AddScoped<TabelaDePrecoRepository>();
        services.AddScoped<ITabelaDePrecoRepository, TabelaDePrecoCached>();

        services.AddScoped<ItensPedidoRepository>();
        services.AddScoped<IItensPedidoRepository, ItensPedidoCached>();
    }
}
