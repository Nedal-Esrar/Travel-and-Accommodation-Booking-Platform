using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace TABP.Application.Extensions.Validation;

/// <summary>
///   Extension methods for FluentValidation to add custom validation rules.
/// </summary>
public static class ValidationExtensions
{
  /// <summary>
  ///   Validates that the string property represents a valid name with specified minimum and maximum lengths.
  /// </summary>
  /// <typeparam name="T">The type of the object being validated.</typeparam>
  /// <param name="ruleBuilder">The rule builder.</param>
  /// <param name="minLength">The minimum length of the name.</param>
  /// <param name="maxLength">The maximum length of the name.</param>
  /// <returns>The rule builder options.</returns>
  /// <remarks>
  ///   The valid name pattern allows alphabetic characters and spaces.
  /// </remarks>
  public static IRuleBuilderOptions<T, string> ValidName<T>(
    this IRuleBuilder<T, string> ruleBuilder, int minLength, int maxLength)
  {
    return ruleBuilder
      .Matches(@"^[A-Za-z(),\-\s]+$")
      .WithMessage(ValidationMessages.NameIsNotValid)
      .Length(minLength, maxLength);
  }

  /// <summary>
  ///   Validates that the string property represents a valid phone number.
  /// </summary>
  /// <typeparam name="T">The type of the object being validated.</typeparam>
  /// <param name="ruleBuilder">The rule builder.</param>
  /// <returns>The rule builder options.</returns>
  /// <remarks>
  ///   The phone number pattern allows various formats including international codes, area codes, and optional separators.
  /// </remarks>
  public static IRuleBuilderOptions<T, string> PhoneNumber<T>(
    this IRuleBuilder<T, string> ruleBuilder)
  {
    return ruleBuilder
      .Matches(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")
      .WithMessage(ValidationMessages.PhoneNumberIsNotValid);
  }

  /// <summary>
  ///   Validates that the string property represents a numeric string with the specified length.
  /// </summary>
  /// <typeparam name="T">The type of the object being validated.</typeparam>
  /// <param name="ruleBuilder">The rule builder.</param>
  /// <param name="length">The exact length of the numeric string.</param>
  /// <returns>The rule builder options.</returns>
  /// <remarks>
  ///   The numeric string pattern allows only digits and must have the specified length.
  /// </remarks>
  public static IRuleBuilderOptions<T, string> ValidNumericString<T>(
    this IRuleBuilder<T, string> ruleBuilder,
    int length)
  {
    return ruleBuilder
      .Matches($"^[0-9]{{{length}}}$")
      .WithMessage(ValidationMessages.GenerateNotANumericStringMessage(length));
  }

  /// <summary>
  ///   Validates that the IFormFile property represents a valid image file.
  /// </summary>
  /// <typeparam name="T">The type of the object being validated.</typeparam>
  /// <param name="ruleBuilder">The rule builder.</param>
  /// <returns>The rule builder options.</returns>
  /// <remarks>
  ///   The valid image types include jpg, jpeg, and png.
  /// </remarks>
  public static IRuleBuilderOptions<T, IFormFile> ValidImage<T>(
    this IRuleBuilder<T, IFormFile> ruleBuilder)
  {
    var allowedImageTypes = new[] { "image/jpg", "image/jpeg", "image/png" };

    return ruleBuilder
      .Must(x => allowedImageTypes.Contains(x.ContentType, StringComparer.OrdinalIgnoreCase))
      .WithMessage(ValidationMessages.NotAnImageFile);
  }

  /// <summary>
  ///   Validates that the string property represents a strong password.
  /// </summary>
  /// <typeparam name="T">The type of the object being validated.</typeparam>
  /// <param name="ruleBuilder">The rule builder.</param>
  /// <returns>The rule builder options.</returns>
  /// <remarks>
  ///   The strong password pattern enforces a combination of uppercase and lowercase letters,
  ///   digits, and special characters, with a minimum length of 8 characters.
  /// </remarks>
  public static IRuleBuilderOptions<T, string> StrongPassword<T>(this IRuleBuilderOptions<T, string> ruleBuilder)
  {
    return ruleBuilder
      .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")
      .WithMessage(ValidationMessages.PasswordIsWeak);
  }
}