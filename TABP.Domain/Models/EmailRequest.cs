namespace TABP.Domain.Models;

public record EmailRequest(
  IEnumerable<string> ToEmails,
  string Subject,
  string Body,
  IEnumerable<(string name, byte[] file)> Attachments);