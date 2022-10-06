namespace Core.Security.AbstractObjects;

public interface IUserForRegister
{
    string Email { get; set; }
    string Password { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
}