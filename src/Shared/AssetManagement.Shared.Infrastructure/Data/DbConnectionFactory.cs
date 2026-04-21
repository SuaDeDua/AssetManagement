using System.Data.Common;
using AssetManagement.Shared.Application;
using Npgsql;

namespace AssetManagement.Shared.Infrastructure.Data;

internal sealed class DbConnectionFactory(NpgsqlDataSource dataSource) : IDbConnectionFactory
{
    public async ValueTask<DbConnection> OpenConnectionAsync()
    {
        return await dataSource.OpenConnectionAsync();
    }
}
