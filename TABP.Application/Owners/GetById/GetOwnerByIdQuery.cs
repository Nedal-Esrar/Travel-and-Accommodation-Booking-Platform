using MediatR;
using TABP.Application.Owners.Common;

namespace TABP.Application.Owners.GetById;

public record GetOwnerByIdQuery(Guid OwnerId) : IRequest<OwnerResponse>;