using Assetly.Modules.Assets.Application.Assets.GetAsset;
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
                    AssetResponse asset = await sender.Send(new GetAssetQuery(id));

                    return asset is null ? Results.NotFound() : Results.Ok(asset);
                }
            )
            .WithTags(Tags.Assets);
    }
}
