namespace TABP.Domain.Messages;

public static class UserMessages
{
  public const string NotFound = "User with provided ID is not found.";
  public const string WithEmailExists = "Another user with the provided email exists.";
  public const string CredentialsNotValid = "The provided credentials are not valid.";
  public const string InvalidRole = "The provided role is invalid.";
  public const string NotAuthenticated = "User is not authenticated.";
  public const string NotGuest = "The authenticated user is not a guest.";
}