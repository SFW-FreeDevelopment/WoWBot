using System.Threading.Tasks;
using Discord.Commands;
using ReminderBot.App.Services;
using WoWBot.App.Commands.Abstractions;

namespace WoWBot.App.Commands.Realm;

public class SetRealmCommand : CommandBase
{
    private readonly IFavoriteService _favoriteService;
    
    public SetRealmCommand(IFavoriteService favoriteService)
    {
        _favoriteService = favoriteService;
    }
    
    [Command("setrealm")]
    public async Task Command([Remainder] string realm)
    {
        await HandleCommandAsync(async () =>
        {
            _favoriteService.SetFavoriteRealm(realm);
            await ReplyAsync($"Favorite realm set to {realm}.");
        });
    }
}