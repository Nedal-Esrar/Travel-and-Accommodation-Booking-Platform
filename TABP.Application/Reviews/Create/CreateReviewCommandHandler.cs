using AutoMapper;
using MediatR;
using TABP.Application.Reviews.Common;
using TABP.Domain.Entities;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;

namespace TABP.Application.Reviews.Create;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ReviewResponse>
{
  private readonly IBookingRepository _bookingRepository;
  private readonly IHotelRepository _hotelRepository;
  private readonly IMapper _mapper;
  private readonly IReviewRepository _reviewRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IUserRepository _userRepository;

  public CreateReviewCommandHandler(
    IHotelRepository hotelRepository,
    IUserRepository userRepository,
    IReviewRepository reviewRepository,
    IBookingRepository bookingRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
  {
    _hotelRepository = hotelRepository;
    _userRepository = userRepository;
    _reviewRepository = reviewRepository;
    _bookingRepository = bookingRepository;
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  public async Task<ReviewResponse> Handle(CreateReviewCommand request,
    CancellationToken cancellationToken = default)
  {
    if (!await _hotelRepository.ExistsByIdAsync(request.HotelId, cancellationToken))
    {
      throw new NotFoundException(HotelMessages.NotFound);
    }

    if (!await _userRepository.ExistsByIdAsync(request.GuestId, cancellationToken))
    {
      throw new NotFoundException(UserMessages.NotFound);
    }

    if (await _bookingRepository.ExistsForHotelAndGuestAsync(request.HotelId, request.GuestId, cancellationToken))
    {
      throw new GuestDidNotBookHotelException(BookingMessages.NoBookingForGuestInHotel);
    }

    if (await _reviewRepository.ExistsByGuestAndHotelIds(request.GuestId, request.HotelId, cancellationToken))
    {
      throw new ReviewAlreadyExistsException(ReviewMessages.GuestAlreadyReviewedHotel);
    }

    var ratingSum = await _reviewRepository.GetTotalRatingForHotelAsync(request.HotelId, cancellationToken);

    var reviewsCount = await _reviewRepository.GetReviewCountForHotelAsync(request.HotelId, cancellationToken);

    ratingSum += request.Rating;
    reviewsCount++;

    var newRating = 1.0 * ratingSum / reviewsCount;

    await _hotelRepository.UpdateReviewById(request.HotelId, newRating, cancellationToken);

    var createdReview = await _reviewRepository
      .CreateAsync(_mapper.Map<Review>(request), cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return _mapper.Map<ReviewResponse>(createdReview);
  }
}