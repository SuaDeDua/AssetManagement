using AssetManagement.Modules.Assets.Application.Data;
using AssetManagement.Modules.Assets.Domain.Assets;
using MediatR;

namespace AssetManagement.Modules.Assets.Application.Assets.CreateAsset;

internal sealed class CreateAssetCommandHandler(
    IAssetRepository assetRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<CreateAssetCommand, Guid>
{
    public async Task<Guid> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
    {
        var asset = Asset.CreateNew(request.Name, request.Description, request.SerialNumber);

        assetRepository.Insert(asset);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return asset.Id;
    }
}
