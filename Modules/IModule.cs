using DragonBot.Instance;

namespace DragonBot.Modules
{
    public interface IModule<T>
    {
        public abstract static string Name { get; }
        public abstract static T Create(Bot bot);
    }
    public interface ICommand
    {
        public void RegisterCommands();
    }
}
