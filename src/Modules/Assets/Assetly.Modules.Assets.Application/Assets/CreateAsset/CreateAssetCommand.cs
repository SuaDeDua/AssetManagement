using MediatR;

namespace Assetly.Modules.Assets.Application.Assets.CreateAsset;

public record CreateAssetCommand(string Name, string Description, string SerialNumber)
    : IRequest<Guid>;
