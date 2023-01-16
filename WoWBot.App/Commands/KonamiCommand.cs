using System.Threading.Tasks;
using Discord.Commands;

namespace ReminderBot.App.Commands;

public class KonamiCommand : CommandBase
{
    [Command("konami")]
    public async Task Command()
    {
        await ReplyAsync("⬆⬆⬇⬇⬅➡⬅➡🅱🅰");
    }
}