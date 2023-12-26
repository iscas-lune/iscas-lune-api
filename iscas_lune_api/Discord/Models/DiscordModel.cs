using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace iscas_lune_api.Discord.Models;

public class DiscordModel
{
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;
    [JsonPropertyName("embeds")]
    public Embeds[]? Embeds { get; set; }

    public StringContent ToJson()
    {
        var json = JsonSerializer.Serialize(this, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = ReferenceHandler.Preserve,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        });

        return new StringContent(
                json,
                Encoding.UTF8,
                "application/json");
    }
}

public class Embeds
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
    [JsonPropertyName("color")]
    public int? Color { get; set; }
}
