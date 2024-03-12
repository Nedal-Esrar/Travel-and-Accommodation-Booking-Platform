using TABP.Domain.Entities;

namespace TABP.Domain.Interfaces.Persistence.Repositories;

public interface IUserRepository
{
  /// <summary>
  ///   returns the user that has the provided credentials, otherwise null.
  /// </summary>
  Task<User?> AuthenticateAsync(string email, string password, CancellationToken cancellationToken = default);

  Task CreateAsync(User user, CancellationToken cancellationToken = default);

  Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
  Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default);
  Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}