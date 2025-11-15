namespace DragonBot.Modules
{
    public interface IModule<T>
    {
        public abstract static string Name { get; }
        public static abstract T Create();
    }
}
