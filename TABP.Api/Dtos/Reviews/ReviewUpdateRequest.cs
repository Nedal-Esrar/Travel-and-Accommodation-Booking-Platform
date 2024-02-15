namespace TABP.Api.Dtos.Reviews;

public record ReviewUpdateRequest(
  string Content,
  int Rating);