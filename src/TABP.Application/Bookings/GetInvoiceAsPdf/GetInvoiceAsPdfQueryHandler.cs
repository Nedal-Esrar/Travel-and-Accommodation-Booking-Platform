using MediatR;
using TABP.Domain;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.Messages;
using static TABP.Application.Bookings.Common.InvoiceDetailsGenerator;

namespace TABP.Application.Bookings.GetInvoiceAsPdf;

public class GetInvoiceAsPdfQueryHandler : IRequestHandler<GetInvoiceAsPdfQuery, byte[]>
{
  private readonly IBookingRepository _bookingRepository;
  private readonly IPdfService _pdfService;
  private readonly IUserRepository _userRepository;
  private readonly IUserContext _userContext;

  public GetInvoiceAsPdfQueryHandler(
    IBookingRepository bookingRepository,
    IPdfService pdfService,
    IUserRepository userRepository,
    IUserContext userContext)
  {
    _bookingRepository = bookingRepository;
    _pdfService = pdfService;
    _userRepository = userRepository;
    _userContext = userContext;
  }

  public async Task<byte[]> Handle(GetInvoiceAsPdfQuery request, CancellationToken cancellationToken = default)
  {
    if (!await _userRepository.ExistsByIdAsync(_userContext.Id, cancellationToken))
    {
      throw new NotFoundException(UserMessages.NotFound);
    }
    
    if (_userContext.Role != UserRoles.Guest)
    {
      throw new ForbiddenException(UserMessages.NotGuest);
    }

    var booking = await _bookingRepository.GetByIdAsync(
                    _userContext.Id,
                    request.BookingId,
                    true,
                    cancellationToken) ??
                  throw new NotFoundException(BookingMessages.NotFoundForGuest);

    return await _pdfService.GeneratePdfFromHtmlAsync(
      GetInvoiceHtml(booking),
      cancellationToken);
  }
}