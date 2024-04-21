using System.Text.Json;
using FluentValidation;

namespace TABP.Infrastructure.Common;

public static class ValidationExtensions
{
  public static IRuleBuilderOptions<T, string> ValidJson<T>(this IRuleBuilder<T, string> ruleBuilder)
  {
    return ruleBuilder
      .Must(IsValidJson)
      .WithMessage(ValidationMessages.InvalidJson);
  }

  private static bool IsValidJson(string json)
  {
    try
    {
      _ = JsonDocument.Parse(json);
      
      return true;
    }
    catch (JsonException)
    {
      return false;
    }
  }
}