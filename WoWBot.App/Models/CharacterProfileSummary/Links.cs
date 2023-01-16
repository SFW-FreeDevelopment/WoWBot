using System.Text.Json.Serialization;

namespace WoWBot.App.Models.CharacterProfileSummary;

public class Links
{
    [JsonPropertyName("self")]
    public Self Self { get; set; }
}