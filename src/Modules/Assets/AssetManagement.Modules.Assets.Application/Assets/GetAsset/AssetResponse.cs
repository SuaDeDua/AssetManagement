namespace AssetManagement.Modules.Assets.Application.Assets.GetAsset;

public sealed record AssetResponse(Guid Id, string Name, string Description, string SerialNumber);
