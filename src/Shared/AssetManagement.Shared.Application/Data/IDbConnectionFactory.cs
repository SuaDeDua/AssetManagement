using System.Data.Common;

namespace AssetManagement.Shared.Application;

public interface IDbConnectionFactory
{
    ValueTask<DbConnection> OpenConnectionAsync();
}
