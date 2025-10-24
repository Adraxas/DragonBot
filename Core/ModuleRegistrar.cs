using Discord;
using DragonBot.Modules;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DragonBot.Core
{
    internal static class ModuleRegistrar
    {
        private static readonly Dictionary<string, System.Delegate> CoreModules = [];
        private static readonly Dictionary<string, System.Delegate> Modules = [];
        public static async Task<RegistrationState> Register(string name, Delegate? module)
        {
            bool Core = default;
            if (CoreModules.ContainsKey(name))
            {
                return RegistrationState.AlreadyRegistered;
            }
            try
            {
                Core = module!.GetMethodInfo().DeclaringType?.GetField("IsCore")?.GetValue(null) is true;
                if (Core)
                {
                    CoreModules.Add(name, module!);
                }
                else
                {
                    Modules.Add(name, module!);
                }
                return RegistrationState.Success;
            }
            catch (Exception ex)
            {
                if (Core)
                {
                    await Program.Log($"Exeption {ex} thrown in regitration for core module {name}. This is a fatal error and should never happen. Program will now exit.", LogSeverity.Critical);
                    Environment.Exit(-1);
                }
                else
                {
                    await Program.Log($"Exeption {ex} thrown in module regitration for {name}.", LogSeverity.Error);
                }
                return RegistrationState.ErrorThrown;
            }
        }
    }
    public enum RegistrationState
    {
        Success,
        ErrorThrown,
        AlreadyRegistered
    }
}
