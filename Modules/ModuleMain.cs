using DragonBot.Core;

namespace DragonBot.Modules
{
    internal sealed class ModuleMain
    {
        private static readonly Dictionary<Type, Action<object>> Initilizers = new()
        {
            { typeof(ICommand), static (module) => ((ICommand)module).RegisterCommands() }
        };

        public static void InitAssembly()
        {
            ModuleRegistrar.Initializers = ModuleRegistrar.Initializers.Merge(Initilizers);
        }
    }
}
