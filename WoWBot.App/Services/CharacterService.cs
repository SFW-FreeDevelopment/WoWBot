using System.Threading.Tasks;
using ReminderBot.App.Clients;
using ReminderBot.App.Models;
using WoWBot.App.Commands.Abstractions;

namespace ReminderBot.App.Services;

public interface ICharacterService
{
    Task<GetCharacterProfileSummaryResponse> GetCharacterProfile(string realm, string character, CommandBase command);
    Task<int> GetAverageItemLevel(string realm, string character, CommandBase command);
    Task<int> GetLevel(string realm, string character, CommandBase command);
    Task<int> GetAchievementPoints(string realm, string character, CommandBase command);
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

    public async Task<GetCharacterProfileSummaryResponse> GetCharacterProfile(string realm, string character, CommandBase command)
    {
        if (string.IsNullOrWhiteSpace(realm))
            realm = _favoriteService.GetFavoriteRealm();
        if (string.IsNullOrWhiteSpace(character))
            character = _favoriteService.GetFavoriteCharacter(command.UserId);
        
        return await _profileClient.GetCharacterProfile(realm, character);
    }
        

    public async Task<int> GetAverageItemLevel(string realm, string character, CommandBase command)
    {
        var profile = await GetCharacterProfile(realm, character, command);
        return profile.EquippedItemLevel;
    }
    
    public async Task<int> GetLevel(string realm, string character, CommandBase command)
    {
        var profile = await GetCharacterProfile(realm, character, command);
        return profile.Level;
    }
    
    public async Task<int> GetAchievementPoints(string realm, string character, CommandBase command)
    {
        var profile = await GetCharacterProfile(realm, character,command);
        return profile.AchievementPoints;
    }
}