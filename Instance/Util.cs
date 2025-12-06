using System;
using System.Collections.Generic;
using System.Text;

namespace DragonBot.Instance
{
    public class Util
    {
        private List<Discord.WebSocket.SocketApplicationCommand> CommandCache { get; init; } = [];
        internal Util(Bot bot)
        {
            CommandCache = bot.Client.GetGuild(bot.BotConfig.GuildId).GetApplicationCommandsAsync().Result.ToList();
        }
        public bool IsCommandRegistered(string commandName)
        {
            return CommandCache.Any(cmd => cmd.Name.Equals(commandName, StringComparison.OrdinalIgnoreCase));
        }

        internal void InitializeModules(Bot bot)
        {
            throw new NotImplementedException();
        }
    }
}
