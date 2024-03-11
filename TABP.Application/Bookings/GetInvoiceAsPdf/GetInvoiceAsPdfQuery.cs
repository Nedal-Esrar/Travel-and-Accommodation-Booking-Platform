using MediatR;

namespace TABP.Application.Bookings.GetInvoiceAsPdf;

public class GetInvoiceAsPdfQuery : IRequest<byte[]>
{
  public Guid GuestId { get; init; }
  public Guid BookingId { get; init; }
}