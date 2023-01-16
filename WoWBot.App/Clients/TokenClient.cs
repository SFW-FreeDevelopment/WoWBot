using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using ReminderBot.App.Models;

namespace ReminderBot.App.Clients;

public interface ITokenClient
{
    Task<string> GetToken();
}

public class TokenClient : ITokenClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly IMemoryCache _memoryCache;
    
    public TokenClient(HttpClient httpClient, IConfiguration configuration, IMemoryCache memoryCache)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _memoryCache = memoryCache;
    }
    
    public async Task<string> GetToken()
    {
        const string cacheKey = "accessToken";
        if (_memoryCache.TryGetValue<string>(cacheKey, out var accessToken)) return accessToken;
        
        var username = _configuration["BattleNet:ClientId"];
        var password = _configuration["BattleNet:ClientSecret"];
        var authorization = Convert.ToBase64String(Encoding.Default.GetBytes($"{username}:{password}"));

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://oauth.battle.net/token"),
            Content = new FormUrlEncodedContent(new []
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            })
        };

        _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authorization);
        
        var response = await _httpClient.SendAsync(request);
        var contentString = await response.Content.ReadAsStringAsync();
        
        var deserializedResponse = JsonSerializer.Deserialize<GetTokenResponse>(contentString);
        _memoryCache.Set(cacheKey, deserializedResponse.AccessToken, TimeSpan.FromHours(12));
        return deserializedResponse.AccessToken;
    }
}