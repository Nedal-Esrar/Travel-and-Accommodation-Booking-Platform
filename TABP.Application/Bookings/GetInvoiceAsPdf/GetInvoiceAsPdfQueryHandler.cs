using MediatR;
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

  public GetInvoiceAsPdfQueryHandler(
    IBookingRepository bookingRepository,
    IPdfService pdfService,
    IUserRepository userRepository)
  {
    _bookingRepository = bookingRepository;
    _pdfService = pdfService;
    _userRepository = userRepository;
  }

  public async Task<byte[]> Handle(GetInvoiceAsPdfQuery request, CancellationToken cancellationToken = default)
  {
    if (!await _userRepository.ExistsByIdAsync(request.GuestId, cancellationToken))
    {
      throw new NotFoundException(UserMessages.NotFound);
    }

    var booking = await _bookingRepository.GetByIdAsync(
                    request.GuestId,
                    request.BookingId,
                    true,
                    cancellationToken) ??
                  throw new NotFoundException(BookingMessages.NotFoundForGuest);

    return await _pdfService.GeneratePdfFromHtmlAsync(
      GetInvoiceHtml(booking),
      cancellationToken);
  }
}