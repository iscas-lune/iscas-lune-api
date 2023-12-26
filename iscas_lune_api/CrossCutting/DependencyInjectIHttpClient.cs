using iscas_lune_api.Discord.Client;
using iscaslune.Api;

namespace iscas_lune_api.CrossCutting;

public static class DependencyInjectIHttpClient
{
    public static void InjectHttpClient(this IServiceCollection services)
    {
        services.AddTransient<IDiscordNotification, DiscordNotification>();
        services.AddHttpClient("Discord", x =>
        {
            x.BaseAddress = new Uri(EnvironmentVariable.GetVariable("URL_DISCORD"));
        });
    }
}
