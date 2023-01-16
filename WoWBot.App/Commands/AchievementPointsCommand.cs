using System.Threading.Tasks;
using Discord.Commands;
using ReminderBot.App.Services;

namespace ReminderBot.App.Commands;

public class AchievementPointsCommand : CommandBase
{
    private readonly ICharacterService _characterService;
    
    public AchievementPointsCommand(ICharacterService characterService)
    {
        _characterService = characterService;
    }
    
    [Command("achievement-points")]
    public async Task Command(string character, [Remainder] string realm = null)
    {
        await HandleCommandAsync(async () =>
        {
            var points = await _characterService.GetAchievementPoints(realm, character);
            await ReplyAsync($"{character} has {points} achievement points.");
        });
    }
}