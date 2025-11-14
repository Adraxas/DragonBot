using Discord;
using DragonBot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DragonBot.Modules
{
    [RegisterModule]
    internal class ButtonMessage : ModuleBase, ICoreModule
    {
        const string Name = "ButtonMessage";

        public static async new Task Register()
        {
            await Program.Log("Derived", LogSeverity.Debug);
        }
        internal static ButtonMessage Create()
        {
            return new ButtonMessage();
        }
    }
}
