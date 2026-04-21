using System.Data.Common;
using AssetManagement.Shared.Application;
using Dapper;
using MediatR;

namespace AssetManagement.Modules.Assets.Application.Assets.GetAsset;

internal sealed class GetAssetQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IRequestHandler<GetAssetQuery, AssetResponse?>
{
    public async Task<AssetResponse?> Handle(
        GetAssetQuery request,
        CancellationToken cancellationToken
    )
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
                //language=sql
                $"""
                 SELECT
                    id AS {nameof(AssetResponse.Id)},
                    name AS {nameof(AssetResponse.Name)},
                    description AS {nameof(AssetResponse.Description)},
                    serial_number AS {nameof(AssetResponse.SerialNumber)}
                 FROM assets.assets
                 WHERE id = @AssetId
                """;

        AssetResponse? asset = await connection.QuerySingleOrDefaultAsync<AssetResponse>(
            sql,
            request
        );

        return asset;
    }
}
