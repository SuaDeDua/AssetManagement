using System.Data.Common;

namespace Assetly.Shared.Application.Data;

public interface IDbConnectionFactory
{
    ValueTask<DbConnection> OpenConnectionAsync();
}
