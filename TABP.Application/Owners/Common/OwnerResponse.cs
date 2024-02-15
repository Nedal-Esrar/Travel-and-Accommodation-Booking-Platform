namespace TABP.Application.Owners.Common;

public record OwnerResponse(
  Guid Id,
  string FirstName,
  string LastName,
  string Email,
  string PhoneNumber);