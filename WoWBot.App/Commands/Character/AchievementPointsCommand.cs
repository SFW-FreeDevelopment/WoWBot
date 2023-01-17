using System.Threading.Tasks;
using Discord.Commands;
using ReminderBot.App.Services;
using WoWBot.App.Commands.Abstractions;

namespace WoWBot.App.Commands.Character;

public class AchievementPointsCommand : CommandBase
{
    private readonly ICharacterService _characterService;
    private readonly IFavoriteService _favoriteService;
    
    public AchievementPointsCommand(ICharacterService characterService, IFavoriteService favoriteService)
    {
        _characterService = characterService;
        _favoriteService = favoriteService;
    }
    
    [Command("achievement-points")]
    public async Task Command(string character = null, [Remainder] string realm = null)
    {
        await HandleCommandAsync(async () =>
        {
            var points = await _characterService.GetAchievementPoints(realm, character, this);
            await ReplyAsync($"{character ?? _favoriteService.GetFavoriteCharacter(UserId)} has {points} achievement points.");
        });
    }
}