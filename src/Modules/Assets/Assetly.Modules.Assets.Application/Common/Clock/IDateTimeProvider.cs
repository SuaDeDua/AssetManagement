namespace Assetly.Modules.Assets.Application.Common.Clock;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}
