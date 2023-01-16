using System.Threading.Tasks;
using Discord.Commands;
using ReminderBot.App.Services;

namespace ReminderBot.App.Commands;

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
        _favoriteService.SetFavoriteRealm(realm);
        await ReplyAsync($"Favorite realm set to {realm}.");
    }
}