using MediatR;
using TABP.Application.Owners.Common;

namespace TABP.Application.Owners.Create;

public record CreateOwnerCommand(
  string FirstName,
  string LastName,
  string Email,
  string PhoneNumber) : IRequest<OwnerResponse>;