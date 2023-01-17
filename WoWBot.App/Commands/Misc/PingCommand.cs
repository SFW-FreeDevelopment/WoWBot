using System.Threading.Tasks;
using Discord.Commands;
using WoWBot.App.Commands.Abstractions;

namespace WoWBot.App.Commands.Misc;

public class PingCommand : CommandBase
{
    [Command("ping")]
    public async Task Command()
    {
        await ReplyAsync("I am pinging the server.");
    }
}