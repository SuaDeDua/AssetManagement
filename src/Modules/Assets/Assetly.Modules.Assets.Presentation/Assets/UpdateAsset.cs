using Assetly.Modules.Assets.Application.Assets.UpdateAsset;
using Assetly.Modules.Assets.Presentation.ApiResults;
using Assetly.Shared.Domain.Common;
using Assetly.Shared.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Assetly.Modules.Assets.Presentation.Assets;

internal sealed class UpdateAsset
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
                "assets/{id}/updates",
                async (Guid id, UpdateAssetRequest request, ISender sender) =>
                {
                    Result result = await sender.Send(
                        new UpdateAssetCommand(id, new Description(request.Description))
                    );

                    return result.Match(Results.NoContent, ApiResults.ApiResults.Problem);
                }
            )
            .WithTags(Tags.Assets);
    }

    internal sealed class UpdateAssetRequest
    {
        public Description Description { get; init; }
    }
}
