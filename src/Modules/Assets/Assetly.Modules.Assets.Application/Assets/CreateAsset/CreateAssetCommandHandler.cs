using Assetly.Modules.Assets.Application.Data;
using Assetly.Modules.Assets.Domain.Assets;
using MediatR;

namespace Assetly.Modules.Assets.Application.Assets.CreateAsset;

internal sealed class CreateAssetCommandHandler(
    IAssetRepository assetRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<CreateAssetCommand, Guid>
{
    public async Task<Guid> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
    {
        var asset = Asset.Create(request.Name, request.Description, request.SerialNumber);

        assetRepository.Insert(asset);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return asset.Id;
    }
}
