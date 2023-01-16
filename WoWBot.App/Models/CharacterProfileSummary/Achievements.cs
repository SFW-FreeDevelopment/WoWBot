using System.Text.Json.Serialization;

namespace WoWBot.App.Models.CharacterProfileSummary;

    public class Achievements
    {
        [JsonPropertyName("href")]
        public string Href { get; set; }
    }