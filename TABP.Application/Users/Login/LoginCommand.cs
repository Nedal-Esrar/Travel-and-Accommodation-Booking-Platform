using MediatR;
using TAABB.Application.Users.Login;

namespace TABP.Application.Users.Login;

public record LoginCommand(
  string Email,
  string Password) : IRequest<LoginResponse>;