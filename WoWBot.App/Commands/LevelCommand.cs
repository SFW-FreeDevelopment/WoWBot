﻿using System.Threading.Tasks;
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
    public async Task Command(string character, [Remainder] string realm = null)
    {
        await HandleCommandAsync(async () =>
        {
            var level = await _characterService.GetLevel(realm, character);
            await ReplyAsync($"{character}'s level is {level}.");
        });
    }
}