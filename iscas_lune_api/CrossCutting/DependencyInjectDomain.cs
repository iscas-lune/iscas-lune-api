using iscaslune.Api.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace iscaslune.Api.CrossCutting;

public static class DependencyInjectDomain
{
    public static void InjectDomain(this IServiceCollection services)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        var connectionString = EnvironmentVariable.GetVariable("PG_STRING");
        services.AddDbContext<IscasLuneContext>(opt =>
                opt.UseNpgsql(connectionString,
                b => b.MigrationsAssembly(typeof(IscasLuneContext).Assembly.FullName)));
        //var conecctionLegado = "User ID=admin; Password=Mm%B2102; Host=postgres-db; Port=54325; Database=201.182.97.170; Pooling=true;";
        //services.AddDbContext<IscasLuneContext_legado>(opt =>
        //        opt.UseNpgsql(conecctionLegado,
        //        b => b.MigrationsAssembly(typeof(IscasLuneContext).Assembly.FullName)));
    }
}
