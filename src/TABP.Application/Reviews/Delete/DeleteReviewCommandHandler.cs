using MediatR;
using TABP.Domain;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.Messages;

namespace TABP.Application.Reviews.Delete;

public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand>
{
  private readonly IHotelRepository _hotelRepository;
  private readonly IReviewRepository _reviewRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IUserRepository _userRepository;
  private readonly IUserContext _userContext;

  public DeleteReviewCommandHandler(
    IHotelRepository hotelRepository,
    IUserRepository userRepository,
    IReviewRepository reviewRepository,
    IUnitOfWork unitOfWork, 
    IUserContext userContext)
  {
    _hotelRepository = hotelRepository;
    _userRepository = userRepository;
    _reviewRepository = reviewRepository;
    _unitOfWork = unitOfWork;
    _userContext = userContext;
  }

  public async Task Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
  {
    if (!await _hotelRepository.ExistsAsync(h => h.Id == request.HotelId, cancellationToken))
    {
      throw new NotFoundException(HotelMessages.NotFound);
    }

    if (!await _userRepository.ExistsByIdAsync(_userContext.Id, cancellationToken))
    {
      throw new NotFoundException(UserMessages.NotFound);
    }
    
    if (_userContext.Role != UserRoles.Guest)
    {
      throw new ForbiddenException(UserMessages.NotGuest);
    }

    var review = await _reviewRepository.GetByIdAsync(request.ReviewId,
                   request.HotelId, _userContext.Id, cancellationToken)
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