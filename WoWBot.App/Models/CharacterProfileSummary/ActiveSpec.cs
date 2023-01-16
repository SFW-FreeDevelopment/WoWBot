using System.Text.Json.Serialization;

namespace WoWBot.App.Models.CharacterProfileSummary;

public class ActiveSpec
{
    [JsonPropertyName("key")]
    public Key Key { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }
}