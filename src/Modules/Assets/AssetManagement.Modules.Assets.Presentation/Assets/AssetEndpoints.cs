using Microsoft.AspNetCore.Routing;

namespace AssetManagement.Modules.Assets.Presentation.Assets;

public static class AssetEndpoints
{
    public static void MapEnpoints(IEndpointRouteBuilder app)
    {
        CreateAsset.MapEndpoint(app);
        GetAsset.MapEndpoint(app);
    }
}
