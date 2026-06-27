using System.Data.Common;
using Assetly.Modules.Assets.Application.Common.Messaging;
using Assetly.Shared.Application.Data;
using Assetly.Shared.Domain.Common;
using Dapper;

namespace Assetly.Modules.Assets.Application.Assets.GetAsset;

internal sealed class GetAssetQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetAssetQuery, AssetResponse>
{
    public async Task<Result<AssetResponse>> Handle(
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

        AssetResponse assetResponse = await connection.QuerySingleOrDefaultAsync<AssetResponse>(
            sql,
            request
        );

        return Result<AssetResponse>.Success(assetResponse!);
    }
}
