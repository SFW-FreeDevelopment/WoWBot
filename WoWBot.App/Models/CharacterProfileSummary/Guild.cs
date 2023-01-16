using System.Text.Json.Serialization;

namespace WoWBot.App.Models.CharacterProfileSummary;

public class Guild
{
    [JsonPropertyName("key")]
    public Key Key { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("realm")]
    public Realm Realm { get; set; }

    [JsonPropertyName("faction")]
    public Faction Faction { get; set; }
}