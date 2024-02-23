namespace TABP.Infrastructure.Common.OptionsValidation;

public static class ValidationMessages
{
  public const string InvalidJson = "'{PropertyName}' is not a valid JSON string.";

  public static string GenerateValidationFailureMessages(string optionsType, string propertyName, string errorMessages)
  {
    return $"Fluent validation failed for '{optionsType}.{propertyName}' with the error: '{errorMessages}'.";
  }
}