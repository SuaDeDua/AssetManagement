using MediatR;

namespace AssetManagement.Modules.Assets.Application.Assets.CreateAsset;

public sealed record CreateAssetCommand(string Name, string Description, string SerialNumber)
    : IRequest<Guid>;
