namespace TABP.Api.Dtos.Auth;

public record LoginRequest(
  string Email,
  string Password);