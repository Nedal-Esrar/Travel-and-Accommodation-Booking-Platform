namespace TABP.Api.Dtos.Owners;

public record OwnerUpdateRequest(
  string FirstName,
  string LastName,
  string Email,
  string PhoneNumber);