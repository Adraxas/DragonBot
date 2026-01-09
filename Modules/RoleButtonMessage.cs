using Discord;
using Discord.Net;
using DragonBot.Core;
using DragonBot.Instance;
using Nito.AsyncEx;
using System;
using System.Text.Json;

namespace DragonBot.Modules
{
    [RegisterModule]
    internal sealed class RoleButtonMessage : ModuleBase, IModule<RoleButtonMessage>
    {
        public static string Name { get; } = "Core:ButtonMessage";

        public static RoleButtonMessage Create(Bot bot)
        {
            return new RoleButtonMessage(bot);
        }
        private RoleButtonMessage(Bot bot) : base(bot)
        {

        }
        public async Task<IMessage> GetOrCreateMessage()
        {
            //var channel = bot.Client.g
            return null!;
        }
        public async override void RegisterCommands()
        {
            if (!bot.Util.IsCommandRegistered("create-role-button-message"))
            {
                var guild = bot.Client.GetGuild(bot.BotConfig.GuildId);
                SlashCommandBuilder builder = new();

                builder.WithName("create-role-button-message")
                    .WithDescription("Creates a message with buttons that add/remove roles")
                    .WithDefaultMemberPermissions(GuildPermission.Administrator)
                    .WithContextTypes(InteractionContextType.Guild);

#pragma warning disable CS0618 // Type or member is obsolete
                try
                {
                    await guild.CreateApplicationCommandAsync(builder.Build());
                }
                catch(ApplicationCommandException ex)
                {
                    var json = JsonSerializer.Serialize(ex.Errors);
                    await Program.Log($@"ApplicationCommandException thrown during command registration for command {builder.Name}
                    Errors reported: {json}", LogSeverity.Error);
                }
                catch(Exception ex)
                {
                    await Program.Log($"Exeption {ex} thrown during command registration for command {builder.Name}", LogSeverity.Error);
                }
#pragma warning restore CS0618 // Type or member is obsolete
            }
        }
    }
}
