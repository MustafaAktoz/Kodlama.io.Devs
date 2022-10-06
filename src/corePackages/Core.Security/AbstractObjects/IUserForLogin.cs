namespace Core.Security.AbstractObjects;

public interface IUserForLogin
{
    string Email { get; set; }
    string Password { get; set; }
    string? AuthenticatorCode { get; set; }
}