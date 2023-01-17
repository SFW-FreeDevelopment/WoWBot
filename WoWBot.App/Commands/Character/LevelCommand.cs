using System.Threading.Tasks;
using Discord.Commands;
using ReminderBot.App.Services;
using WoWBot.App.Commands.Abstractions;

namespace WoWBot.App.Commands.Character;

public class LevelCommand : CommandBase
{
    private readonly ICharacterService _characterService;
    private readonly IFavoriteService _favoriteService;
    
    public LevelCommand(ICharacterService characterService, IFavoriteService favoriteService)
    {
        _characterService = characterService;
        _favoriteService = favoriteService;
    }
    
    [Command("level")]
    public async Task Command(string character = null, [Remainder] string realm = null)
    {
        await HandleCommandAsync(async () =>
        {
            var level = await _characterService.GetLevel(realm, character, this);
            await ReplyAsync($"{character ?? _favoriteService.GetFavoriteCharacter(UserId)}'s level is {level}.");
        });
    }
}