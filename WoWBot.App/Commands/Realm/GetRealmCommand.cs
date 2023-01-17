using System.Threading.Tasks;
using Discord.Commands;
using ReminderBot.App.Services;
using WoWBot.App.Commands.Abstractions;

namespace WoWBot.App.Commands.Realm;

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
        await HandleCommandAsync(async () =>
        {
            var realm = _favoriteService.GetFavoriteRealm();
            await ReplyAsync($"Favorite realm is {realm}.");
        });
    }
}