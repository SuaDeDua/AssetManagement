using Assetly.Modules.Assets.Application.Assets.CreateAsset;
using Assetly.Modules.Assets.Presentation.ApiResults;
using Assetly.Shared.Domain.Common;
using Assetly.Shared.Domain.ValueObjects;
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
                    Result<Guid> result = await sender.Send(
                        new CreateAssetCommand(
                            request.Name,
                            request.Description,
                            request.SerialNumber
                        )
                    );

                    return result.Match(Results.Ok, ApiResults.ApiResults.Problem);
                }
            )
            .WithTags(Tags.Assets);
    }

    internal sealed class Request
    {
        public string Name { get; set; }
        public Description Description { get; set; }
        public string SerialNumber { get; set; }
    }
}
