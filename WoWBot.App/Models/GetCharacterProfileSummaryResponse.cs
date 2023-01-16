using System.Text.Json.Serialization;
using WoWBot.App.Models.CharacterProfileSummary;

namespace ReminderBot.App.Models;

public class GetCharacterProfileSummaryResponse
{
    [JsonPropertyName("_links")] public Links Links { get; set; }

    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("gender")] public Gender Gender { get; set; }

    [JsonPropertyName("faction")] public Faction Faction { get; set; }

    [JsonPropertyName("race")] public Race Race { get; set; }

    [JsonPropertyName("character_class")] public CharacterClass CharacterClass { get; set; }

    [JsonPropertyName("active_spec")] public ActiveSpec ActiveSpec { get; set; }

    [JsonPropertyName("realm")] public Realm Realm { get; set; }

    [JsonPropertyName("guild")] public Guild Guild { get; set; }

    [JsonPropertyName("level")] public int Level { get; set; }

    [JsonPropertyName("experience")] public int Experience { get; set; }

    [JsonPropertyName("achievement_points")]
    public int AchievementPoints { get; set; }

    [JsonPropertyName("achievements")] public Achievements Achievements { get; set; }

    [JsonPropertyName("titles")] public Titles Titles { get; set; }

    [JsonPropertyName("pvp_summary")] public PvpSummary PvpSummary { get; set; }

    [JsonPropertyName("encounters")] public Encounters Encounters { get; set; }

    [JsonPropertyName("media")] public Media Media { get; set; }

    [JsonPropertyName("last_login_timestamp")]
    public long LastLoginTimestamp { get; set; }

    [JsonPropertyName("average_item_level")]
    public int AverageItemLevel { get; set; }

    [JsonPropertyName("equipped_item_level")]
    public int EquippedItemLevel { get; set; }

    [JsonPropertyName("specializations")] public Specializations Specializations { get; set; }

    [JsonPropertyName("statistics")] public Statistics Statistics { get; set; }

    [JsonPropertyName("mythic_keystone_profile")]
    public MythicKeystoneProfile MythicKeystoneProfile { get; set; }

    [JsonPropertyName("equipment")] public Equipment Equipment { get; set; }

    [JsonPropertyName("appearance")] public Appearance Appearance { get; set; }

    [JsonPropertyName("collections")] public Collections Collections { get; set; }

    [JsonPropertyName("active_title")] public ActiveTitle ActiveTitle { get; set; }

    [JsonPropertyName("reputations")] public Reputations Reputations { get; set; }

    [JsonPropertyName("quests")] public Quests Quests { get; set; }

    [JsonPropertyName("achievements_statistics")]
    public AchievementsStatistics AchievementsStatistics { get; set; }

    [JsonPropertyName("professions")] public Professions Professions { get; set; }
}