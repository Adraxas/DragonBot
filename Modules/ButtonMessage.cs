using Discord;
using DragonBot.Core;
using Nito.AsyncEx;

namespace DragonBot.Modules
{
    [RegisterModule]
    internal class ButtonMessage : ModuleBase, IModule<ButtonMessage>
    {
        public static string Name { get; } = "Core:ButtonMessage";

        public static ButtonMessage Create()
        {
            AsyncContext.Run(() => Program.Log("Derived", LogSeverity.Debug));
            return new ButtonMessage();
        }
    }
}
