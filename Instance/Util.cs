namespace DragonBot.Instance
{
    public class Util
    {
        private readonly Bot bot;
        internal Util(Bot bot)
        {
            this.bot = bot;
        }
        public bool IsCommandRegistered(string commandName)
        {
            return bot.Client.GetGuild(bot.BotConfig.GuildId).GetApplicationCommandsAsync().Result
                .Any(cmd => cmd.Name.Equals(commandName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
