using MediatR;
using TABP.Application.Owners.Common;

namespace TABP.Application.Owners.GetById;

public class GetOwnerByIdQuery : IRequest<OwnerResponse>
{
  public Guid OwnerId { get; init; }
}