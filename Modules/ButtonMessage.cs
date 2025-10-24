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
    internal class ButtonMessage : ModuleBase, ICoreModule
    {
        const string Name = "ButtonMessage";
        
        internal static void reg()
        {
            ButtonMessage.Register();
        }
        internal new static ButtonMessage Create()
        {
            return new ButtonMessage();
        }
    }
}
