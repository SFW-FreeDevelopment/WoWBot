using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using ReminderBot.App.Exceptions;

namespace ReminderBot.App.Commands;

public abstract class CommandBase : ModuleBase<SocketCommandContext>
{
    protected SocketUser User => Context.Message.Author;
    protected IGuildUser GuildUser => (IGuildUser)User;
    protected string Mention => User.Mention;

    protected async Task HandleCommandAsync(Func<Task> action)
    {
        try
        {
            await action();
        }
        catch (BattleNetApiException e)
        {
            await ReplyAsync(e.Message);
        }
        catch (Exception)
        {
            await ReplyAsync("An unknown error has occurred.");
        }
    }
}