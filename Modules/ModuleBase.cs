using DragonBot.Instance;

namespace DragonBot.Modules
{
    public abstract class ModuleBase
    {
        public List<string> Dependecies { get; } = [];
        protected readonly Bot bot;
        protected ModuleBase(Bot bot)
        {
            this.bot = bot;
        }
    }
}
