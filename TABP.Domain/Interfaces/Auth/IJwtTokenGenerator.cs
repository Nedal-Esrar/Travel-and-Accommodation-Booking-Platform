using TABP.Domain.Entities;
using TABP.Domain.Models;

namespace TABP.Domain.Interfaces.Auth;

public interface IJwtTokenGenerator
{
  JwtToken Generate(User user);
}