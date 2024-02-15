using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Auth;
using TABP.Domain.Models;

namespace TABP.Infrastructure.Auth.Jwt;

public class JwtTokenGenerator : IJwtTokenGenerator
{
  private readonly JwtAuthConfig _jwtAuthConfig;

  public JwtTokenGenerator(IOptions<JwtAuthConfig> jwtAuthConfig)
  {
    _jwtAuthConfig = jwtAuthConfig.Value;
  }

  public JwtToken Generate(User user)
  {
    var claims = new List<Claim>
    {
      new("sub", user.Id.ToString()),
      new("firstName", user.FirstName),
      new("lastName", user.LastName),
      new("email", user.Email)
    };

    claims.AddRange(user.Roles.Select(r => new Claim(ClaimTypes.Role, r.Name)));

    var signingCredentials = new SigningCredentials(
      new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtAuthConfig.Key)),
      SecurityAlgorithms.HmacSha256);

    var jwtSecurityToken = new JwtSecurityToken(
      _jwtAuthConfig.Issuer,
      _jwtAuthConfig.Audience,
      claims,
      DateTime.UtcNow,
      DateTime.UtcNow.AddMinutes(_jwtAuthConfig.LifetimeMinutes),
      signingCredentials
    );

    var token = new JwtSecurityTokenHandler()
      .WriteToken(jwtSecurityToken);

    return new JwtToken(token);
  }
}