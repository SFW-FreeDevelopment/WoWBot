using System.Text.Json.Serialization;

namespace WoWBot.App.Models.CharacterProfileSummary;

public class Equipment
{
    [JsonPropertyName("href")]
    public string Href { get; set; }
}