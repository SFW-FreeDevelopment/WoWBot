using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using ReminderBot.App.Clients;
using ReminderBot.App.Models;

namespace ReminderBot.App.Services;

public interface ICharacterService
{
    Task<GetCharacterProfileSummaryResponse> GetCharacterProfile(string realm, string character);
    Task<int> GetAverageItemLevel(string realm, string character);
    Task<int> GetLevel(string realm, string character);
    Task<int> GetAchievementPoints(string realm, string character);
}

public class CharacterService : ICharacterService
{
    private readonly IProfileClient _profileClient;
    private readonly IFavoriteService _favoriteService;
    
    public CharacterService(IProfileClient profileClient, IFavoriteService favoriteService)
    {
        _profileClient = profileClient;
        _favoriteService = favoriteService;
    }

    public async Task<GetCharacterProfileSummaryResponse> GetCharacterProfile(string realm, string character)
    {
        if (string.IsNullOrWhiteSpace(realm)) realm = _favoriteService.GetFavoriteRealm();
        return await _profileClient.GetCharacterProfile(realm, character);
    }
        

    public async Task<int> GetAverageItemLevel(string realm, string character)
    {
        var profile = await GetCharacterProfile(realm, character);
        return profile.EquippedItemLevel;
    }
    
    public async Task<int> GetLevel(string realm, string character)
    {
        var profile = await GetCharacterProfile(realm, character);
        return profile.Level;
    }
    
    public async Task<int> GetAchievementPoints(string realm, string character)
    {
        var profile = await GetCharacterProfile(realm, character);
        return profile.AchievementPoints;
    }
}