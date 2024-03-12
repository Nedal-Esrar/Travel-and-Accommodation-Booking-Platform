using MediatR;

namespace TABP.Application.Owners.Update;

public class UpdateOwnerCommand : IRequest
{
  public Guid OwnerId { get; init; }
  public string FirstName { get; init; }
  public string LastName { get; init; }
  public string Email { get; init; }
  public string PhoneNumber { get; init; }
}