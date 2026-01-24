using Discord;
using Discord.WebSocket;

namespace DragonBot.Instance
{
    public sealed class RoleManager
    {
        private readonly Bot bot;
        private SocketGuild Guild { get; }
        internal RoleManager(Bot bot)
        {
            this.bot = bot;
            Guild = bot.Client.GetGuild(bot.BotConfig.GuildId);
        }
        public static async Task AddRole(IGuildUser User, IRole role)
        {
            await User.AddRoleAsync(role);
        }
        public static async Task RemoveRole(IGuildUser User, IRole role)
        {
            await User.RemoveRoleAsync(role);
        }
        public async Task<bool> HasRole(ulong UserId, IRole role)
        {
            var user = Guild.GetUser(UserId);
            return user.Roles.Contains(role);
        }
        public async Task<string> IdToName(ulong roleId)
        {
            var role = bot.Client.GetGuild(bot.BotConfig.GuildId).GetRole(roleId);
            return role.Name;
        }
        public async Task<ulong> NameToId(string roleName)
        {
            var role = bot.Client.GetGuild(bot.BotConfig.GuildId).Roles.FirstOrDefault(r => r.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase));
            return role?.Id ?? 0;
        }
    }
}
