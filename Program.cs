using Discord;
using DragonBot.Instance;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DragonBot
{
    public static class Program
    {
        //Should never be null after Init();
        internal static GlobalSettings? Settings;
        public static async Task Main(string[] args)
        {
            await Init();
            if (Environment.GetCommandLineArgs().Contains("-init"))
            {
                Environment.Exit(0);
            }
            await Task.Delay(-1);
        }
        private static async Task Init()
        {
            string DefaultBaseDir = AppContext.BaseDirectory;
#if DEBUG
            File.Delete($"{DefaultBaseDir}/settings.json");
            Directory.Delete($"{DefaultBaseDir}/logs", true);
            Directory.Delete($"{DefaultBaseDir}/instances", true);
#endif
            if (File.Exists($"{DefaultBaseDir}/settings.json"))
            {
                using StreamReader r = new($"{DefaultBaseDir}/settings.json");
                string json = r.ReadToEnd();
                Settings = JsonSerializer.Deserialize<GlobalSettings>(json) ?? new() { BaseDir = DefaultBaseDir };
            }
            else
            {
                Settings = new() { BaseDir = DefaultBaseDir };
                await using StreamWriter w = new($"{AppContext.BaseDirectory}/settings.json");
                await w.WriteAsync(JsonSerializer.Serialize(Settings));
            }
            Directory.CreateDirectory(Settings.InstanceConfigsDir);
            var configs = Directory.EnumerateFiles(Settings.InstanceConfigsDir);
            if (configs.Any())
            {
                foreach (var config in configs)
                {
                    await Bot.Create(Path.GetFileNameWithoutExtension(config));
                }
            }
            else
            {
                await Bot.Create("DragonBot");
            }
        }
        internal static async Task Log(LogMessage logMessage)
        {
            Directory.CreateDirectory(Settings!.LogDir);
            await using StreamWriter outputFile = new(Path.Combine(Settings!.LogDir, "latest.log"));
            await outputFile.WriteAsync($"{DateTime.Now} {logMessage.Severity}:{logMessage.Message}");
            await Task.CompletedTask;
        }
        internal record GlobalSettings([property: JsonPropertyName("singleInstance")] bool SingleInstance = true)
        {
            [property: JsonPropertyName("baseDirectory")]
            internal required string BaseDir { get; init; }
            [property: JsonPropertyName("logDirectory")]
            internal string LogDir { get => field ??= $"{BaseDir}/logs"; init; }
            [property: JsonPropertyName("instanceConfigsDirectory")]
            internal string InstanceConfigsDir { get => field ??= $"{BaseDir}/instances"; init; }
        }
    }
}
