using System;
using System.Collections.Generic;
using System.Text;

namespace DragonBot.Instance
{
    public class Util
    {
        private readonly Bot bot;
        private List<Discord.WebSocket.SocketApplicationCommand> CommandCache { get; set; } = [];
        internal Util(Bot bot)
        {
            this.bot = bot;
            RefreshCommandCache();
        }
        public void RefreshCommandCache()
        {
            CommandCache = bot.Client.GetGuild(bot.BotConfig.GuildId).GetApplicationCommandsAsync().Result.ToList();
        }
        public bool IsCommandRegistered(string commandName)
        {
            RefreshCommandCache();
            return CommandCache.Any(cmd => cmd.Name.Equals(commandName, StringComparison.OrdinalIgnoreCase));
        }

        internal void InitializeModules(Bot bot)
        {
            throw new NotImplementedException();
        }
    }
}
