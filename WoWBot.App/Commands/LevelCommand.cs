using System.Threading.Tasks;
using Discord.Commands;
using ReminderBot.App.Services;

namespace ReminderBot.App.Commands;

public class LevelCommand : CommandBase
{
    private readonly ICharacterService _characterService;
    
    public LevelCommand(ICharacterService characterService)
    {
        _characterService = characterService;
    }
    
    [Command("level")]
    public async Task HandleCommandAsync(string character, [Remainder] string realm)
    {
        var profile = await _characterService.GetCharacterProfile(realm, character);
        await ReplyAsync($"{character}'s level is {profile.Level}.");
    }
}