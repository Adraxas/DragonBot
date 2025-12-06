using DragonBot.Instance;

namespace DragonBot.Modules
{
    public abstract class ModuleBase
    {
        protected readonly Bot bot;
        protected ModuleBase(Bot bot)
        {
            this.bot = bot;
            RegisterCommands();
        }
        public virtual void RegisterCommands()
        {
            // Override in derived classes to register commands
        }
    }
}
