using Assetly.Shared.Kernel.Common;

namespace Assetly.Api;

internal sealed class FakeCurrentUser : ICurrentUser
{
    public Guid UserId => Guid.NewGuid();

    public Guid CustomerId => Guid.NewGuid();

    public string UserName => "Test";
}
