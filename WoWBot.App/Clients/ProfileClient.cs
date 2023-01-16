using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Caching.Memory;
using ReminderBot.App.Models;

namespace ReminderBot.App.Clients;

public interface IProfileClient
{
    Task<GetCharacterProfileSummaryResponse> GetCharacterProfile(string realm, string character);
}

public class ProfileClient : IProfileClient
{
    private readonly HttpClient _httpClient;
    private readonly ITokenClient _tokenClient;
    private readonly IMemoryCache _memoryCache;
    
    public ProfileClient(HttpClient httpClient, ITokenClient tokenClient, IMemoryCache memoryCache)
    {
        _httpClient = httpClient;
        _tokenClient = tokenClient;
        _memoryCache = memoryCache;
    }
    
    public async Task<GetCharacterProfileSummaryResponse> GetCharacterProfile(string realm, string character)
    {
        var cacheKey = $"character:profile:{character}-{realm}";
        if (_memoryCache.TryGetValue<GetCharacterProfileSummaryResponse>(cacheKey, out var cachedResponse)) return cachedResponse;
        
        realm = HttpUtility.UrlEncode(realm.ToLower());
        character = HttpUtility.UrlEncode(character.ToLower());
        
        var token = await _tokenClient.GetToken();
        
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://us.api.blizzard.com/profile/wow/character/{realm}/{character}?namespace=profile-us&locale=en_US&access_token={token}")
        };
        
        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode) return null;

        var contentString = await response.Content.ReadAsStringAsync();
        
        var deserializedResponse = JsonSerializer.Deserialize<GetCharacterProfileSummaryResponse>(contentString);
        _memoryCache.Set(cacheKey, deserializedResponse, TimeSpan.FromMinutes(10));
        return deserializedResponse;
    }
}