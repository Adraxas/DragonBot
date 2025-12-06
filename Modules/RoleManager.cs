using Discord;
using Discord.WebSocket;
using DragonBot.Core;
using DragonBot.Instance;
using Nito.AsyncEx;

namespace DragonBot.Modules
{
    [RegisterModule]
    internal sealed class RoleManager : ModuleBase, IModule<RoleManager>
    {
        public static string Name { get; } = "Core:RoleManager";
        public static RoleManager Create(Bot bot)
        {
            return new RoleManager(bot);
        }
        private RoleManager(Bot bot) : base(bot)
        {
        }
        public async Task AddRole(IGuildUser User, IRole role)
        {
            await User.AddRoleAsync(role);
        }
        public async Task RemoveRole(IGuildUser User, IRole role)
        {
            await User.RemoveRoleAsync(role);
        }
        public async Task<bool> HasRole(ulong UserId, IRole role)
        {
            //var user = bot.Client.GetGuild(bot.BotConfig.GuildId).GetUser(UserId);
            //return user.Roles.Contains(role);
            return false;
        }
    }
}
