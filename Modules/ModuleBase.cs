using DragonBot.Instance;

namespace DragonBot.Modules
{
    public abstract class ModuleBase
    {
        protected readonly Bot bot;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0290:Use primary constructor", Justification = "Might Need to add more to it later")]
        protected ModuleBase(Bot bot)
        {
            this.bot = bot;
        }
    }
}
