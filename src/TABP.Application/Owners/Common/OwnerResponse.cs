namespace TABP.Application.Owners.Common;

public class OwnerResponse
{
  public Guid Id { get; init; }
  public string FirstName { get; init; }
  public string LastName { get; init; }
  public string Email { get; init; }
  public string PhoneNumber { get; init; }
}