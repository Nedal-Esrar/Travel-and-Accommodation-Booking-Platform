namespace TABP.Domain.Entities;

public class User : EntityBase
{
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string Email { get; set; }
  public string Password { get; set; }
  public ICollection<Role> Roles { get; set; } = new List<Role>();
}