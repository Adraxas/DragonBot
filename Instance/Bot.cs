using System.Text.Json;
using static DragonBot.Program;
using Discord.WebSocket;
using Discord;
using System.Text.Json.Serialization;




#if DEBUG
using Microsoft.Extensions.Configuration;
#endif

namespace DragonBot.Instance
{
    internal sealed class Bot
    {
        internal readonly BotConfig botConfig;
        private readonly DiscordSocketClient client = new();
        private Bot(string botName, string? token)
        {
            string DefaultToken = "";
#if DEBUG
            IConfigurationRoot config = new ConfigurationBuilder()
            .AddUserSecrets<Bot>()
            .Build();
            DefaultToken = config.GetSection("BotToken").Value!;
#endif
            if (File.Exists($"{Settings!.InstanceConfigsDir}/{botName}.json"))
            {
                using StreamReader r = new($"{Settings!.InstanceConfigsDir}/{botName}.json");
                string json = r.ReadToEnd();
                botConfig = JsonSerializer.Deserialize<BotConfig>(json) ?? new();
            }
            else
            {
                botConfig = new() { Token = token ?? DefaultToken};
                using StreamWriter w = new($"{Settings!.InstanceConfigsDir}/{botName}.json");
                w.Write(JsonSerializer.Serialize(botConfig));
            }
        }
        internal static async Task<Bot> Create(string botName, string? token = null)
        {
            Bot bot = new(botName, token);
            bot.client.Log += Log;
            await bot.client.LoginAsync(TokenType.Bot, bot.botConfig.Token);
            await bot.client.StartAsync();
            return bot;
        }
        
    }
    internal record BotConfig([property: JsonPropertyName("loggingEnabled")] bool Logging = true, [property: JsonPropertyName("DiscordToken")] string? Token = null);
}