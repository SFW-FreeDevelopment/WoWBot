using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using ReminderBot.App.Exceptions;

namespace WoWBot.App.Commands.Abstractions;

public abstract class CommandBase : ModuleBase<SocketCommandContext>
{
    public SocketUser User => Context.Message.Author;
    public IGuildUser GuildUser => (IGuildUser)User;
    public string Mention => User.Mention;
    public string UserId => User.Id.ToString();

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
        catch (FavoriteException e)
        {
            await ReplyAsync(e.Message);
        }
        catch (Exception)
        {
            await ReplyAsync("An unknown error has occurred.");
        }
    }
}