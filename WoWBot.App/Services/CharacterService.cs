using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using ReminderBot.App.Models;

namespace ReminderBot.App.Services;

public interface ICharacterService
{
    Task<GetCharacterProfileSummaryResponse> GetCharacterProfile(string realm, string character);
    Task<int> GetAverageItemLevel(string realm, string character);
}

public class CharacterService : ICharacterService
{
    private readonly HttpClient _httpClient;
    private readonly ITokenService _tokenService;
    
    public CharacterService(HttpClient httpClient, ITokenService tokenService)
    {
        _httpClient = httpClient;
        _tokenService = tokenService;
    }

    public async Task<GetCharacterProfileSummaryResponse> GetCharacterProfile(string realm, string character)
    {
        realm = HttpUtility.UrlEncode(realm.ToLower());
        character = HttpUtility.UrlEncode(character.ToLower());
        
        var token = await _tokenService.GetToken();
        
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://us.api.blizzard.com/profile/wow/character/{realm}/{character}?namespace=profile-us&locale=en_US&access_token={token}")
        };
        
        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode) return null;

        var contentString = await response.Content.ReadAsStringAsync();
        
        var deserializedResponse = JsonSerializer.Deserialize<GetCharacterProfileSummaryResponse>(contentString);
        return deserializedResponse;
    }

    public async Task<int> GetAverageItemLevel(string realm, string character)
    {
        var characterProfileSummary = await GetCharacterProfile(realm, character);
        return characterProfileSummary.EquippedItemLevel;
    }
}