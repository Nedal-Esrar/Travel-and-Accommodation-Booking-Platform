using AutoMapper;
using MediatR;
using TABP.Domain;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.Messages;

namespace TABP.Application.Reviews.Update;

public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand>
{
  private readonly IHotelRepository _hotelRepository;
  private readonly IMapper _mapper;
  private readonly IReviewRepository _reviewRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IUserRepository _userRepository;
  private readonly IUserContext _userContext;

  public UpdateReviewCommandHandler(
    IHotelRepository hotelRepository,
    IUserRepository userRepository,
    IReviewRepository reviewRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper, 
    IUserContext userContext)
  {
    _hotelRepository = hotelRepository;
    _userRepository = userRepository;
    _reviewRepository = reviewRepository;
    _unitOfWork = unitOfWork;
    _mapper = mapper;
    _userContext = userContext;
  }

  public async Task Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
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

    ratingSum += request.Rating - review.Rating;

    var newRating = 1.0 * ratingSum / reviewsCount;

    await _hotelRepository.UpdateReviewById(request.HotelId,
      newRating, cancellationToken);

    await _reviewRepository.UpdateAsync(
      _mapper.Map(request, review), cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}