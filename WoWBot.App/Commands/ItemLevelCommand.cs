using System.Threading.Tasks;
using Discord.Commands;
using ReminderBot.App.Services;

namespace ReminderBot.App.Commands;

public class ItemLevelCommand : CommandBase
{
    private readonly ICharacterService _characterService;
    
    public ItemLevelCommand(ICharacterService characterService)
    {
        _characterService = characterService;
    }
    
    [Command("ilvl")]
    public async Task HandleCommandAsync(string character, [Remainder] string realm)
    {
        var level = await _characterService.GetAverageItemLevel(realm, character);
        await ReplyAsync($"{character}'s average equipped item level is {level}.");
    }
}