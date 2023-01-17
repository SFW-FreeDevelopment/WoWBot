using System.Threading.Tasks;
using Discord.Commands;
using WoWBot.App.Commands.Abstractions;

namespace WoWBot.App.Commands.Misc;

public class KonamiCommand : CommandBase
{
    [Command("konami")]
    public async Task Command()
    {
        await ReplyAsync("⬆⬆⬇⬇⬅➡⬅➡🅱🅰");
    }
}