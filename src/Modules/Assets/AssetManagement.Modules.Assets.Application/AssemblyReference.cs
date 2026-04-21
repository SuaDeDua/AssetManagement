using System.Reflection;

namespace AssetManagement.Modules.Assets.Application;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
