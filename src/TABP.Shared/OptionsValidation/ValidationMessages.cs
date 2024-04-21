namespace TABP.Shared.OptionsValidation;

public static class ValidationMessages
{
  public static string GenerateValidationFailureMessages(string optionsType, string propertyName, string errorMessages)
  {
    return $"Fluent validation failed for '{optionsType}.{propertyName}' with the error: '{errorMessages}'.";
  }
}