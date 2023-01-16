using System;
using System.Threading.Tasks;
using Discord.Commands;
using ReminderBot.App.Exceptions;
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
    public async Task Command(string character, [Remainder] string realm = null)
    {
        await HandleCommandAsync(async () =>
        {
            var level = await _characterService.GetAverageItemLevel(realm, character);
            await ReplyAsync($"{character}'s average equipped item level is {level}.");
        });
    }
}