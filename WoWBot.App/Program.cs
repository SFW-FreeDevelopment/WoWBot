using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReminderBot.App.Clients;
using ReminderBot.App.Services;

namespace ReminderBot.App
{
    class Program
    {
        static void Main(string[] args)
        {
            RunBotAsync().GetAwaiter().GetResult();
            Console.ReadKey();
        }

        private static DiscordSocketClient _client;
        private static CommandService _commands;
        private static IServiceProvider _services;

        public static async Task RunBotAsync()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
                .AddSingleton<IConfiguration>(_ => configuration)
                .AddScoped<ITokenClient, TokenClient>()
                .AddScoped<IProfileClient, ProfileClient>()
                .AddScoped<ICharacterService, CharacterService>()
                .AddTransient<HttpClient>()
                .AddMemoryCache()
                .BuildServiceProvider();

            //new ReminderService(reminderRepository).CheckReminders().SafeFireAndForget();

            _client.Log += Log;

            await RegisterCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, configuration["DiscordBotToken"]);

            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private static Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }
        
        private static async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        private static async Task HandleCommandAsync(SocketMessage socketMessage)
        {
            var message = socketMessage as SocketUserMessage;
            var context = new SocketCommandContext(_client, message);
            if (message.Author.IsBot) return;

            int argPos = 0;
            if (message.HasStringPrefix("wowbot ", ref argPos))
            {
                await Execute();
                return;
            }
            argPos = 0;
            if (message.HasStringPrefix("<@1064410926289260594> ", ref argPos))
            {
                await Execute();
                return;
            }

            async Task Execute()
            {
                var result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess)
                    Console.WriteLine(result.ErrorReason);
            }
        }
    }
}