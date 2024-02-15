using AutoMapper;
using MediatR;
using TABP.Application.Reviews.Common;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;

namespace TABP.Application.Reviews.GetById;

public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, ReviewResponse>
{
  private readonly IHotelRepository _hotelRepository;
  private readonly IMapper _mapper;
  private readonly IReviewRepository _reviewRepository;

  public GetReviewByIdQueryHandler(
    IHotelRepository hotelRepository,
    IReviewRepository reviewRepository,
    IMapper mapper)
  {
    _hotelRepository = hotelRepository;
    _reviewRepository = reviewRepository;
    _mapper = mapper;
  }

  public async Task<ReviewResponse> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
  {
    if (!await _hotelRepository.ExistsByIdAsync(request.HotelId, cancellationToken))
    {
      throw new NotFoundException(HotelMessages.NotFound);
    }

    var review = await _reviewRepository.GetByIdAsync(
                   request.HotelId, request.ReviewId, cancellationToken) ??
                 throw new NotFoundException(ReviewMessages.WithIdNotFoundInHotelWithId);

    return _mapper.Map<ReviewResponse>(review);
  }
}