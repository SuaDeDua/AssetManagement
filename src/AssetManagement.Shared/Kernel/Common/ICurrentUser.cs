namespace AssetManagement.Shared.Kernel.Common;

public interface ICurrentUser
{
    Guid UserId { get; }

    Guid CustomerId { get; }

    string UserName { get; }
}
