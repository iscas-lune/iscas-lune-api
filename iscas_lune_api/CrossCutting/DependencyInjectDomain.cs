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
    }
}
