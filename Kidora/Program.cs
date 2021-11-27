using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;


namespace Discord
{
    public class Program : ModuleBase<SocketCommandContext>
    {
        public static string Sql_ID;
        public static string Sql_PW;
        public static string Sql_Server;
        public static string Sql_DB;
        public static string token;
        public static ulong Global;
        public static ulong Unique;
        public DiscordSocketClient _client;
        public CommandService _commands;
        public IServiceProvider _services;

        static void Main(string[] args)
        {
            ReadyUP();
           new Program().RunBotAsync().GetAwaiter().GetResult();
        }

        public static void ReadyUP()
        {
            try
            {
                if (!File.Exists("Config.txt"))
                {
                    StreamWriter Channel_ID = new StreamWriter("Config.txt");
                    Channel_ID.WriteLine("GlobalChannel_ID:");
                    Channel_ID.WriteLine("UniqueChannel_ID:");
                    Channel_ID.WriteLine("Token:");
                    Channel_ID.WriteLine("Sql_Server:");
                    Channel_ID.WriteLine("Sql_ID:");
                    Channel_ID.WriteLine("Sql_PW:");
                    Channel_ID.WriteLine("DB:");
                    Channel_ID.Close();
                    Console.WriteLine("Please Refresh the app");
                    return;
                }
                if (File.Exists("Config.txt"))
                {
                    StreamReader Channel_IDRead = new StreamReader("Config.txt");
                    Global = ulong.Parse(Channel_IDRead.ReadLine().Split(':')[1].Trim());
                    Unique = ulong.Parse(Channel_IDRead.ReadLine().Split(':')[1].Trim());
                    token = Channel_IDRead.ReadLine().Split(':')[1].Trim();
                    Channel_IDRead.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task RunBotAsync()
        {
                _client = new DiscordSocketClient();
                _commands = new CommandService();
                _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            _client.Log += _client_Log;

                await RegisterCommandsAsync();

                await _client.LoginAsync(TokenType.Bot, token);

                await _client.StartAsync();

                await Task.Delay(-1);
        }
        private Task _client_Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(_client, message);
            if (message.Author.IsBot) return;

            int argPos = 0;
            if (message.HasStringPrefix("!", ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess) Console.WriteLine(result.ErrorReason);
                if (result.Error.Equals(CommandError.UnmetPrecondition)) await message.Channel.SendMessageAsync(result.ErrorReason);
            }
        }
    }
}
