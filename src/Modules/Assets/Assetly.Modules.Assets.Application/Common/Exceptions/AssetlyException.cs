using Assetly.Shared.Domain.Common;

namespace Assetly.Modules.Assets.Application.Common.Exceptions;

#pragma warning disable RCS1194
public sealed class AssetlyException : Exception
{
    public AssetlyException(
        string requestName,
        Error? error = default,
        Exception? innerException = default
    )
        : base("Application exception", innerException)
    {
        RequestName = requestName;
        Error = error;
    }

    public string RequestName { get; }
    public Error? Error { get; }
}
#pragma warning restore RCS1194
