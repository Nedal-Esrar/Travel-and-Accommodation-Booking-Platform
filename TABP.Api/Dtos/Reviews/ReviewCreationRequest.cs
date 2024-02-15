namespace TABP.Api.Dtos.Reviews;

public record ReviewCreationRequest(
  string Content,
  int Rating);