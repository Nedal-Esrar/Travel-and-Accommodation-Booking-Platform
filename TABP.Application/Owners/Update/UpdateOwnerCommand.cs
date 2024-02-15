using MediatR;

namespace TABP.Application.Owners.Update;

public record UpdateOwnerCommand(
  Guid OwnerId,
  string FirstName,
  string LastName,
  string Email,
  string PhoneNumber) : IRequest;