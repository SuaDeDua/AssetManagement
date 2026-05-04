using Assetly.Modules.Assets.Application.Assets.CreateAsset;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Assetly.Modules.Assets.Presentation.Assets;

internal static class CreateAsset
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "assets",
                async (Request request, ISender sender) =>
                {
                    var command = new CreateAssetCommand(
                        request.Name,
                        request.Description,
                        request.SerialNumber
                    );

                    Guid assetId = await sender.Send(command);

                    return Results.Ok(assetId);
                }
            )
            .WithTags(Tags.Assets);
    }

    internal sealed class Request
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
    }
}
