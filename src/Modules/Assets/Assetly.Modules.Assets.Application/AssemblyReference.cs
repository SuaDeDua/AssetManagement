using System.Reflection;

namespace Assetly.Modules.Assets.Application;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
