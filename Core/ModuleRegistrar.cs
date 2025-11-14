using Discord;
using DragonBot.Modules;
using HarmonyLib;
using System.Reflection;


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
                    await Program.Log($"Exeption {ex} thrown in registration for core module {name}. This is a fatal error and should never happen. Program will now exit.", LogSeverity.Critical);
                    Environment.Exit(-1);
                }
                else
                {
                    await Program.Log($"Exeption {ex} thrown in module registration for {name}.", LogSeverity.Error);
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
    [AttributeUsage(AttributeTargets.Class)]
    public class RegisterModuleAttribute : Attribute
    {
        public static async Task RegisterModulesAsync()
        {
            var targets = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => x.IsClass && x.IsSubclassOf(typeof(MethodBase)) && x.GetCustomAttributes(typeof(RegisterModuleAttribute), false).Length != 0);

            foreach (var target in targets)
            {
                var method = AccessTools.DeclaredMethod(target, "Register");
                if(method != null)
                {
                    var result = method.Invoke(null, null);
                    if(result is Task task)
                    {
                        await task;
                    }
                }
                //await target.DeclaredMethod("Register").Invoke(null, null);
            }
        }
        /*private async void test()
        {
            var state = await ModuleRegistrar.Register(meta.Name, MethodBase.GetCurrentMethod()?.DeclaringType?.GetMethod("Create")?.CreateDelegate<Action>());
            if (state == RegistrationState.Success)
            {
                await Program.Log($"{Name} registered successfully.", LogSeverity.Info);
            }
            else
            {
                
            }
        }*/
    }
    [HarmonyPatch(typeof(ModuleBase))]
    [HarmonyPatch(nameof(ModuleBase.Register))]
    internal class ModuleInitilaizer()
    {
        internal static void Patch()
        {
            var harmony = new Harmony("ModuleInit");
            harmony.PatchAll();
        }
        internal static void Prefix(ModuleBase __originalMethod)
        {
            _ = Program.Log("HarmonyPatch", LogSeverity.Debug);
        }
    }
}
