namespace TABP.Api.Dtos.Auth;

public class RegisterRequest
{
  public string FirstName { get; init; }
  public string LastName { get; init; }
  public string Email { get; init; }
  public string Password { get; init; }
}