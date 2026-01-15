using Discord;
using DragonBot.Modules;
using HarmonyLib;
using Nito.AsyncEx;
using System.Reflection;


namespace DragonBot.Core
{
    internal static class ModuleRegistrar
    {
        private static readonly Dictionary<string, Delegate> Modules = [];
        internal static async Task<RegistrationState> Register(string name, Delegate module)
        {
            if (Modules.ContainsKey(name))
            {
                return RegistrationState.AlreadyRegistered;
            }
            try
            {
                Type moduleClassType = module.GetMethodInfo().DeclaringType ?? throw new ModuleRegistrationExeption("Error getting declared type of module.", true);
                var dependecies = AccessTools.DeclaredField(moduleClassType, "Dependecies");
                await Program.Log($"Sucessfully registered module {name}.", LogSeverity.Info);
                return RegistrationState.Success;
            }
            catch (Exception ex)
            {
                if (ex is ModuleRegistrationExeption exeption)
                {
                    if (exeption.Fatal)
                    {
                        await Program.Log($"ModuleRegistrationExeption thrown in registration of module {name} with reason {ex.Message}. This is a fatal error and should never happen. Program will now exit.", LogSeverity.Critical);
                        Environment.Exit(-1);
                    }
                    await Program.Log($"ModuleRegistrationExeption thrown in registration of module {name} with reason {ex.Message}.", LogSeverity.Error);
                }
                else if (name.StartsWith("Core:"))
                {
                    await Program.Log($"Exeption {ex} thrown in registration for core module {name}. This is a fatal error and should never happen. Program will now exit.", LogSeverity.Critical);
                    Environment.Exit(-1);
                }
                else
                {
                    await Program.Log($"Exeption {ex} thrown in registration for module {name}.", LogSeverity.Error);
                }
                return RegistrationState.ErrorThrown;
            }
        }
        internal static async Task GetRequestedModules()
        {
            return;
        }
    }
    public enum RegistrationState
    {
        Success,
        ErrorThrown,
        AlreadyRegistered,
        MissingDependencies
    }
    [AttributeUsage(AttributeTargets.Class)]
    public class RegisterModuleAttribute : Attribute
    {
        public static void RegisterModules()
        {
            var targets = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => x.IsClass && x.IsSubclassOf(typeof(ModuleBase)) && typeof(IModule<>).IsAssignableFrom(x) && x.GetCustomAttributes(typeof(RegisterModuleAttribute), false).Length != 0);

            foreach (var target in targets)
            {
                var name = AccessTools.DeclaredField(target, "Name").GetValue(null) as string;
                var createMethod = Delegate.CreateDelegate(target, AccessTools.DeclaredMethod("Create"));
                if(name is null || createMethod is null)
                {
                    AsyncContext.Run(() => Program.Log($"Invalid Module (Name:{name} createMethod:{createMethod}).", LogSeverity.Error));
                }
                else
                {
                    switch (AsyncContext.Run(() => ModuleRegistrar.Register(name, createMethod)))
                    {
                        case RegistrationState.Success:
                            break;
                        case RegistrationState.ErrorThrown:
                            break;
                        case RegistrationState.AlreadyRegistered:
                            AsyncContext.Run(() => Program.Log($"Module {name} has already been registered. Did you forget to namespace your modules name. (ex: yourname:modulename)", LogSeverity.Warning));
                            break;
                        case RegistrationState.MissingDependencies:

                            break;
                        default:
                            throw new NotImplementedException();
                    }
                    
                }
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
    /*[HarmonyPatch(typeof(ModuleBase))]
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
            //Program.Log("HarmonyPatch", LogSeverity.Debug);
            AsyncContext.Run(() => Program.Log("HarmonyPatch", LogSeverity.Debug));
        }
    }*/
    [Serializable]
    internal class ModuleRegistrationExeption : Exception
    {
        public bool Fatal { get; }
        private ModuleRegistrationExeption()
        {
        }
        public ModuleRegistrationExeption(string? message) : base(message)
        {
        }
        public ModuleRegistrationExeption(string? message, bool fatal) : base(message)
        {
            Fatal = fatal;
        }
        public ModuleRegistrationExeption(string? message, Exception? innerException) : base(message, innerException)
        {
        }
        public ModuleRegistrationExeption(string? message, Exception? innerException, bool fatal) : base(message, innerException)
        {
            Fatal = fatal;
        }
    }
}
