namespace TABP.Application.Extensions.Validation;

public static class ValidationMessages
{
  public const string NameIsNotValid = "'{PropertyName}' should only contain letters and spaces.";
  public const string PhoneNumberIsNotValid = "'{PropertyName}' Must be a valid phone number.";
  public const string PasswordIsWeak =
    "'{PropertyName}' must be a strong password containing at least one lowercase letter, one uppercase letter, one digit, one special character, and have a minimum length of 8 characters.";
  public const string NotAnImageFile =
    "'{PropertyName}' should be an image of type jpg, jpeg, or png.";

  public static string GenerateNotANumericStringMessage(int length)
  {
    return "'{PropertyName}' " + $"must be exactly {length}-digits.";
  }
}