using Microsoft.Extensions.Caching.Memory;

namespace ReminderBot.App.Services;

public interface IFavoriteService
{
    string GetFavoriteRealm();
    void SetFavoriteRealm(string realm);
}

public class FavoriteService : IFavoriteService
{
    private readonly IMemoryCache _memoryCache;
    
    public FavoriteService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }
    
    public string GetFavoriteRealm() =>
        _memoryCache.TryGetValue<string>("favorite:realm", out var realm) ? realm : "not set";

    public void SetFavoriteRealm(string realm) =>
        _memoryCache.Set("favorite:realm", realm);
}