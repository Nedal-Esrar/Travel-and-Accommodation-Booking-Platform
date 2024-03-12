namespace TABP.Infrastructure.Services.Email;

public class EmailConfig
{
  public required string Server { get; set; }
  public required int Port { get; set; }
  public required string Username { get; set; }
  public required string Password { get; set; }
  public required string FromEmail { get; set; }
}