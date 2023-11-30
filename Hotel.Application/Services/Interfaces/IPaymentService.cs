using Hotel.Application.DTOS.PaymentDto;
using Hotel.Domain.Entities;

namespace Hotel.Application.Services.Interfaces;

public interface IPaymentService
{
    void PayFor(PaymentDto paymentDto);
    IEnumerable<PaymentType> GetPaymentTypes();
    IEnumerable<PaymentListingDto> GetAllPayments();
}