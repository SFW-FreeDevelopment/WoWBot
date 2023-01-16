using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using ReminderBot.App.Clients;
using ReminderBot.App.Models;

namespace ReminderBot.App.Services;

public interface ICharacterService
{
    Task<int> GetAverageItemLevel(string realm, string character);
    Task<int> GetLevel(string realm, string character);
}

public class CharacterService : ICharacterService
{
    private readonly IProfileClient _profileClient;
    
    public CharacterService(IProfileClient profileClient, IMemoryCache memoryCache)
    {
        _profileClient = profileClient;
    }
    
    public async Task<GetCharacterProfileSummaryResponse> GetCharacterProfile(string realm, string character)
    {
        return await _profileClient.GetCharacterProfile(realm, character);
    }
    
    public async Task<int> GetAverageItemLevel(string realm, string character)
    {
        var profile = await _profileClient.GetCharacterProfile(realm, character);
        return profile.EquippedItemLevel;
    }
    
    public async Task<int> GetLevel(string realm, string character)
    {
        var profile = await _profileClient.GetCharacterProfile(realm, character);
        return profile.Level;
    }
}