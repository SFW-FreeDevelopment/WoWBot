using System.Threading.Tasks;
using Discord.Commands;
using ReminderBot.App.Services;
using WoWBot.App.Commands.Abstractions;

namespace WoWBot.App.Commands.Character;

public class GetCharacterCommand : CommandBase
{
    private readonly IFavoriteService _favoriteService;
    
    public GetCharacterCommand(IFavoriteService favoriteService)
    {
        _favoriteService = favoriteService;
    }
    
    [Command("getcharacter")]
    public async Task Command()
    {
        await HandleCommandAsync(async () =>
        {
            var character = _favoriteService.GetFavoriteCharacter(UserId);
            await ReplyAsync($"Favorite character is {character}.");
        });
    }
}