using AssetManagement.Domain.Shared.Common;
using AssetManagement.Domain.Shared.ValueObjects;
using AssetManagement.Domain.Users.Events;
using AssetManagement.Domain.Users.ValueObjects;

namespace AssetManagement.Domain.Users;

public sealed class User : AggregateRoot<Guid>
{
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public EmailAddress Email { get; private set; }

    private User() { }

    private User(Guid id, FirstName firstName, LastName lastName, EmailAddress email)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public static User Create(FirstName firstName, LastName lastName, EmailAddress email)
    {
        var user = new User(Guid.CreateVersion7(), firstName, lastName, email);

        user.AddDomainEvent(new UserCreatedDomainEvent(user.Id));

        return user;
    }
}
