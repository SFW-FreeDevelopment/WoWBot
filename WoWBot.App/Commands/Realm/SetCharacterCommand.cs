using System.Threading.Tasks;
using Discord.Commands;
using ReminderBot.App.Services;
using WoWBot.App.Commands.Abstractions;

namespace WoWBot.App.Commands.Character;

public class SetCharacterCommand : CommandBase
{
    private readonly IFavoriteService _favoriteService;
    
    public SetCharacterCommand(IFavoriteService favoriteService)
    {
        _favoriteService = favoriteService;
    }
    
    [Command("setcharacter")]
    public async Task Command([Remainder] string character)
    {
        await HandleCommandAsync(async () =>
        {
            _favoriteService.SetFavoriteCharacter(UserId, character);
            await ReplyAsync($"Favorite character set to {character}.");
        });
    }
}