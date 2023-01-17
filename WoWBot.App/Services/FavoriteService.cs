using Microsoft.Extensions.Caching.Memory;
using ReminderBot.App.Exceptions;

namespace ReminderBot.App.Services;

public interface IFavoriteService
{
    string GetFavoriteRealm();
    void SetFavoriteRealm(string realm);
    string GetFavoriteCharacter(string discordId);
    void SetFavoriteCharacter(string discordId, string character);
}

public class FavoriteService : IFavoriteService
{
    private readonly IMemoryCache _memoryCache;
    
    public FavoriteService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public string GetFavoriteRealm() => GetFavorite<string>("realm");
    public void SetFavoriteRealm(string realm) => SetFavorite(realm, "realm");
    
    public string GetFavoriteCharacter(string discordId) => GetFavorite<string>($"character:{discordId}");
    public void SetFavoriteCharacter(string discordId, string character) => SetFavorite(character, $"character:{discordId}");

    private void SetFavorite<T>(T favorite, string name) =>
        _memoryCache.Set($"favorite:{name}", favorite);
    
    private T GetFavorite<T>(string name) =>
        _memoryCache.TryGetValue<T>($"favorite:{name}", out var favorite)
            ? favorite
            : throw new FavoriteException(name);
}