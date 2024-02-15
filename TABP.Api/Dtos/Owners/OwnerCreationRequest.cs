namespace TABP.Api.Dtos.Owners;

public record OwnerCreationRequest(
  string FirstName,
  string LastName,
  string Email,
  string PhoneNumber);