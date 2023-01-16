using System.Threading.Tasks;
using Discord.Commands;

namespace ReminderBot.App.Commands;

public class PingCommand : CommandBase
{
    [Command("ping")]
    public async Task Command()
    {
        await ReplyAsync("I am pinging the server.");
    }
}