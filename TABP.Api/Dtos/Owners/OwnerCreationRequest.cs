namespace TABP.Api.Dtos.Owners;

public class OwnerCreationRequest
{
  public string FirstName { get; init; }
  public string LastName { get; init; }
  public string Email { get; init; }
  public string PhoneNumber { get; init; }
}