namespace AssetManagement.Domain.Assets.ValueObjects;

public enum AssetStatus
{
    Available = 1,
    InUse = 2,
    Maintenance = 3, //Bảo hành
    Broken = 4,
    Lost = 5,
    Liquidated = 6, // Thanh lý
}
