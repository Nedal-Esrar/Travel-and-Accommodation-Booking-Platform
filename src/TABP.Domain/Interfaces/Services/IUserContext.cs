namespace TABP.Domain.Interfaces.Services;

public interface IUserContext
{
  Guid Id { get; }
  
  string Role { get; }
  
  string Email { get; }
}