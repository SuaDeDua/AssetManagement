using System.ComponentModel.DataAnnotations;
using AssetManagement.Domain.Shared.Common;
using AssetManagement.Domain.Shared.ValueObjects;

namespace AssetManagement.Domain.Assets;

public class Manufacturer : Entity<Guid>
{
    public Name Name { get; private set; }
    public string ServiceCenter { get; private set; }
    public string UrlHomePage { get; private set; }
    public ImageUrl ImageUrl { get; private set; }
}
