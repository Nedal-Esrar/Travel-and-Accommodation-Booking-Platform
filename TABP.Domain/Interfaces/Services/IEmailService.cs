using TABP.Domain.Models;

namespace TABP.Domain.Interfaces.Services;

public interface IEmailService
{
  Task SendAsync(EmailRequest emailRequest, CancellationToken cancellationToken = default);
}