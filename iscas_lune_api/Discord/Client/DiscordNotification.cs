using iscas_lune_api.Discord.Models;
using System.Text.Json;
using System.Text;
using iscaslune.Api;
using System.Text.Json.Serialization;
using System.Net.Http.Headers;

namespace iscas_lune_api.Discord.Client;

public class DiscordNotification : IDiscordNotification
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _url =
        $"{EnvironmentVariable.GetVariable("DISCORD_WEB_HOOK_ID")}/{EnvironmentVariable.GetVariable("DISCORD_WEB_HOOK_TOKEN")}";

    public DiscordNotification(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task NotifyAsync(DiscordModel discordModel)
    {
        var httpClient = _httpClientFactory.CreateClient("Discord");
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var test = await httpClient.PostAsync(_url, discordModel.ToJson())
            ?? throw new Exception();

        await test.Content.ReadAsStringAsync();
    }
}
