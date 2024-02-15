using MediatR;

namespace TABP.Application.Users.Register;

public record RegisterCommand(
  string FirstName,
  string LastName,
  string Email,
  string Password,
  string Role) : IRequest;