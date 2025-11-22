using System.Text.Json;
using static DragonBot.Program;
using Discord.WebSocket;
using Discord;
using System.Text.Json.Serialization;
using DragonBot.Core;





#if DEBUG
using Microsoft.Extensions.Configuration;
#endif

namespace DragonBot.Instance
{
    public sealed class Bot
    {
        public BotConfig BotConfig { get; init; }
        public DiscordSocketClient Client { get; } = new();
        public MicroBus Bus { get; } = new();
        private Bot(string botName, string? token)
        {
            string DefaultToken = "";
#if DEBUG
            IConfigurationRoot config = new ConfigurationBuilder()
            .AddUserSecrets<Bot>()
            .Build();
            DefaultToken = config.GetSection("BotToken").Value!;
#endif
            if (File.Exists(Path.Combine(Settings!.InstanceConfigDir, botName, ".json")))
            {
                using StreamReader r = new(Path.Combine(Settings!.InstanceConfigDir, botName, ".json"));
                string json = r.ReadToEnd();
                BotConfig = JsonSerializer.Deserialize<BotConfig>(json) ?? new();
            }
            else
            {
                BotConfig = new() { Token = token ?? DefaultToken};
                using StreamWriter w = new(Path.Combine(Settings!.InstanceConfigDir, botName, ".json"));
                w.Write(JsonSerializer.Serialize(BotConfig));
            }
        }
        internal static async Task<Bot> Create(string botName, string? token = null)
        {
            Bot bot = new(botName, token);
            bot.Client.Log += Log;
            await bot.Client.LoginAsync(TokenType.Bot, bot.BotConfig.Token);
            await bot.Client.StartAsync();
            return bot;
        }
    }
    public record BotConfig([property: JsonPropertyName("LoggingEnabled")] bool Logging = true, [property: JsonPropertyName("GuildID")] ulong GuildID = 0, [property: JsonPropertyName("DiscordToken")] string? Token = null)
    {
        [property: JsonPropertyName("ModuleConfigs")]
        Dictionary<string, object> Configs { get; } = [];
    };
}