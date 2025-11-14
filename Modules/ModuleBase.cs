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
    internal abstract class ModuleBase
    {
        const string Name = "ModuleBase";
        public static async void Register()
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }
    }
}
