using Discord;
using DragonBot.Core;
using Nito.AsyncEx;

namespace DragonBot.Modules
{
    [RegisterModule]
    internal class ButtonMessage : ModuleBase, ICoreModule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1213:Remove unused member declaration", Justification = "Member is accessed via reflection")]
        const string Name = "Core:ButtonMessage";

        public static new void Register()
        {
            AsyncContext.Run(() => Program.Log("Derived", LogSeverity.Debug));
        }
        internal static ButtonMessage Create()
        {
            return new ButtonMessage();
        }
    }
}
