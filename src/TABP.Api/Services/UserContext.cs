using System.Security.Claims;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.Messages;

namespace TABP.Api.Services;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
  public Guid Id => Guid.Parse(
      httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ??
      throw new UnauthorizedException(UserMessages.NotAuthenticated));

  public string Role => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role) ??
                        throw new UnauthorizedException(UserMessages.NotAuthenticated);

  public string Email => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email) ??
                         throw new UnauthorizedException(UserMessages.NotAuthenticated);
}