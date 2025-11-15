namespace DragonBot.Modules
{
    public interface IModule
    {
        public static abstract void Register();
    }
    internal interface ICoreModule : IModule
    {
        internal const bool IsCore = true;
    }
}
