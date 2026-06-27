using Assetly.Modules.Assets.Application.Assets.GetAsset;
using Assetly.Modules.Assets.Presentation.ApiResults;
using Assetly.Shared.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Assetly.Modules.Assets.Presentation.Assets;

internal static class GetAsset
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "assets/{id}",
                async (Guid id, ISender sender) =>
                {
                    Result<AssetResponse> result = await sender.Send(new GetAssetQuery(id));

                    return result.Match(Results.Ok, ApiResults.ApiResults.Problem);
                }
            )
            .WithTags(Tags.Assets);
    }
}
