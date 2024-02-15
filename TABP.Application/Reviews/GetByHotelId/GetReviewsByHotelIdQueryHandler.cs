using AutoMapper;
using MediatR;
using TABP.Application.Reviews.Common;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;
using TABP.Domain.Models;

namespace TABP.Application.Reviews.GetByHotelId;

public class GetReviewsByHotelIdQueryHandler : IRequestHandler<GetReviewsByHotelIdQuery, PaginatedList<ReviewResponse>>
{
  private readonly IHotelRepository _hotelRepository;
  private readonly IMapper _mapper;
  private readonly IReviewRepository _reviewRepository;

  public GetReviewsByHotelIdQueryHandler(
    IReviewRepository reviewRepository,
    IMapper mapper,
    IHotelRepository hotelRepository)
  {
    _reviewRepository = reviewRepository;
    _mapper = mapper;
    _hotelRepository = hotelRepository;
  }

  public async Task<PaginatedList<ReviewResponse>> Handle(
    GetReviewsByHotelIdQuery request,
    CancellationToken cancellationToken)
  {
    if (!await _hotelRepository.ExistsByIdAsync(request.HotelId, cancellationToken))
    {
      throw new NotFoundException(HotelMessages.NotFound);
    }

    var query = new PaginationQuery<Review>(
      r => r.HotelId == request.HotelId,
      request.SortOrder ?? SortOrder.Ascending,
      request.SortColumn,
      request.PageNumber,
      request.PageSize);

    var owners = await _reviewRepository.GetAsync(query,
      cancellationToken);

    return _mapper.Map<PaginatedList<ReviewResponse>>(owners);
  }
}