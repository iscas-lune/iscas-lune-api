using iscas_lune_api.Discord.Models;

namespace iscas_lune_api.Discord.Client;

public interface IDiscordNotification
{
    Task NotifyAsync(DiscordModel discordModel);
}
