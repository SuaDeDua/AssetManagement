using Microsoft.AspNetCore.Routing;

namespace Assetly.Modules.Assets.Presentation.Assets;

public static class AssetEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        CreateAsset.MapEndpoint(app);
        GetAsset.MapEndpoint(app);
    }
}
