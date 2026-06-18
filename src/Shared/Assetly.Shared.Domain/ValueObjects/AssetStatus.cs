using Assetly.Shared.Domain.Common;

namespace Assetly.Shared.Domain.ValueObjects;

public class AssetStatus : Enumeration<AssetStatus>
{
    public static readonly AssetStatus Available = new(1, "Available");

    private AssetStatus(int value, string name)
        : base(value, name) { }
    // Available = 1,
    // InUse = 2,
    // Maintenance = 3, //Bảo hành
    // Broken = 4,
    // Lost = 5,
    // Liquidated = 6, // Thanh lý
}
