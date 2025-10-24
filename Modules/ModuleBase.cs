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
    internal abstract class ModuleBase : IModule
    {
        const string Name = "ModuleBase";
        public static async void Register()
        {
            var state = await ModuleRegistrar.Register(Name, MethodBase.GetCurrentMethod()?.DeclaringType?.GetMethod("Create")?.CreateDelegate<Action>());
            if (state == RegistrationState.Success)
            {
                await Program.Log($"{Name} registered successfully.", LogSeverity.Info);
            }
            else
            {

            }
        }
        internal static ModuleBase Create()
        {
            throw new NotImplementedException();
        }
    }
}
