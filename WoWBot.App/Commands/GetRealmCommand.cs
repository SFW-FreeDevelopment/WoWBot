using System.Threading.Tasks;
using Discord.Commands;
using ReminderBot.App.Services;

namespace ReminderBot.App.Commands;

public class GetRealmCommand : CommandBase
{
    private readonly IFavoriteService _favoriteService;
    
    public GetRealmCommand(IFavoriteService favoriteService)
    {
        _favoriteService = favoriteService;
    }
    
    [Command("getrealm")]
    public async Task Command()
    {
        var realm = _favoriteService.GetFavoriteRealm();
        await ReplyAsync($"Favorite realm is {realm}.");
    }
}