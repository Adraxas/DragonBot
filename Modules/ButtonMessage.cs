using Discord;
using DragonBot.Core;
using DragonBot.Instance;
using Nito.AsyncEx;

namespace DragonBot.Modules
{
    [RegisterModule]
    internal sealed class ButtonMessage : ModuleBase, IModule<ButtonMessage>
    {
        public static string Name { get; } = "Core:ButtonMessage";

        public static ButtonMessage Create(Bot bot)
        {
            return new ButtonMessage(bot);
        }
        private ButtonMessage(Bot bot) : base(bot)
        {
        }
    }
}
