namespace DragonBot.Modules
{
    internal abstract class ModuleBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1213:Remove unused member declaration", Justification = "Member is accessed via reflection")]
        const string Name = "ModuleBase";
        public static void Register()
        {
            throw new NotImplementedException();
        }
    }
}
