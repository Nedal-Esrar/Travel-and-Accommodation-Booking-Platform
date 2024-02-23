using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Infrastructure.Persistence.DbContexts;

namespace TABP.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
  private readonly HotelBookingDbContext _context;
  private readonly IPasswordHasher _passwordHasher;

  public UserRepository(
    HotelBookingDbContext context,
    IPasswordHasher passwordHasher)
  {
    _context = context;
    _passwordHasher = passwordHasher;
  }

  public async Task<User?> AuthenticateAsync(
    string email, string password,
    CancellationToken cancellationToken = default)
  {
    var user = await _context.Users
      .Include(u => u.Roles)
      .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

    if (user is null) return null;

    var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user.Password, password);

    return passwordVerificationResult == PasswordVerificationResult.Success
      ? user : null;
  }

  public async Task CreateAsync(User user, 
    CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(user);

    user.Password = _passwordHasher.HashPassword(user.Password);

    await _context.Users.AddAsync(user, cancellationToken);
  }

  public async Task<bool> ExistsByEmailAsync(string email, 
    CancellationToken cancellationToken = default)
  {
    return await _context.Users.AnyAsync(u => u.Email == email, cancellationToken);
  }

  public async Task<bool> ExistsByIdAsync(Guid id, 
    CancellationToken cancellationToken = default)
  {
    return await _context.Users
      .AnyAsync(u => u.Id == id, cancellationToken);
  }

  public async Task<User?> GetByIdAsync(Guid id, 
    CancellationToken cancellationToken = default)
  {
    return await _context.Users
      .FindAsync([id], cancellationToken);
  }
}