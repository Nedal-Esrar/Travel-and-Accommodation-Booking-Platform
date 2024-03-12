using MediatR;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;

namespace TABP.Application.Reviews.Delete;

public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand>
{
  private readonly IHotelRepository _hotelRepository;
  private readonly IReviewRepository _reviewRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IUserRepository _userRepository;

  public DeleteReviewCommandHandler(
    IHotelRepository hotelRepository,
    IUserRepository userRepository,
    IReviewRepository reviewRepository,
    IUnitOfWork unitOfWork)
  {
    _hotelRepository = hotelRepository;
    _userRepository = userRepository;
    _reviewRepository = reviewRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
  {
    if (!await _hotelRepository.ExistsByIdAsync(request.HotelId, cancellationToken))
    {
      throw new NotFoundException(HotelMessages.NotFound);
    }

    if (!await _userRepository.ExistsByIdAsync(request.GuestId, cancellationToken))
    {
      throw new NotFoundException(UserMessages.NotFound);
    }

    var review = await _reviewRepository.GetByIdAsync(request.ReviewId,
                   request.HotelId, request.GuestId, cancellationToken)
                 ?? throw new NotFoundException(ReviewMessages.NotFoundForUserForHotel);

    var ratingSum = await _reviewRepository.GetTotalRatingForHotelAsync(request.HotelId, cancellationToken);

    var reviewsCount = await _reviewRepository.GetReviewCountForHotelAsync(request.HotelId, cancellationToken);

    ratingSum -= review.Rating;
    reviewsCount--;

    var newRating = 1.0 * ratingSum / reviewsCount;

    await _hotelRepository.UpdateReviewById(request.HotelId, newRating, cancellationToken);

    await _reviewRepository.DeleteAsync(request.ReviewId, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}