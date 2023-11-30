using System.Collections;
using Hotel.Domain.Entities;

namespace Hotel.Domain.IRepositories;

public interface IPaymentRepository
{
    Task PayFor(Payment paymentDto);
    IEnumerable<PaymentType> GetPaymentTypes();
    IEnumerable<Payment> GetAllPayments();
}