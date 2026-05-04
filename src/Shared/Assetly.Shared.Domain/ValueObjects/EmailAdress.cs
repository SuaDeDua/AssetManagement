using System.Net.Mail;

namespace Assetly.Shared.Domain.ValueObjects;

public record EmailAdress
{
    public string Value { get; }

    public EmailAdress(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value), "Email cannot be empty");

        if (!IsValidEmail(value))
            throw new ArgumentException("Invalid email format", nameof(value));

        Value = value;
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public static implicit operator string(EmailAdress email) => email.Value;

    public static explicit operator EmailAdress(string email) => new(email);
}
