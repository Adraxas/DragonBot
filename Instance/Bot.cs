
using System.Text.Json;
using static DragonBot.Program;

namespace DragonBot
{
    internal sealed class Bot
    {
        internal readonly BotConfig botConfig;
        private Bot(string botName)
        {
            if (File.Exists($"{Settings!.InstanceConfigsDir}/{botName}.json"))
            {
                using StreamReader r = new($"{Settings!.InstanceConfigsDir}/{botName}.json");
                string json = r.ReadToEnd();
                botConfig = JsonSerializer.Deserialize<BotConfig>(json) ?? new();
            }
            else
            {
                botConfig = new();
                using StreamWriter w = new($"{Settings!.InstanceConfigsDir}/{botName}.json");
                w.Write(JsonSerializer.Serialize(Settings));
            }
        }
        internal static async Task<Bot> Create(string botName)
        {
            Bot bot = new(botName);

            return bot;
        }
        
    }
    internal record BotConfig(bool Logging = false, string Key = "");
}