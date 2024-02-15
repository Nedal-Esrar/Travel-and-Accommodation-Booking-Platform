using MediatR;

namespace TABP.Application.Bookings.GetInvoiceAsPdf;

public record GetInvoiceAsPdfQuery(
  Guid GuestId,
  Guid BookingId) : IRequest<byte[]>;