using System.Collections;
using Hotel.Domain.Entities;
using Hotel.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly HotelDbContext _dbContext;
    
    public PaymentRepository(HotelDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task PayFor(Payment payment)
    {
        var reservation = _dbContext.Reservations.FirstOrDefault(a=> a.Id == payment.ReservationId);
        reservation.ReservationStatusId = 2;
        _dbContext.Update(reservation);
        _dbContext.Add(payment);
        await _dbContext.SaveChangesAsync();
    }

    public IEnumerable<PaymentType> GetPaymentTypes()
    {
        return _dbContext.PaymentTypes.ToList();
    }

    public IEnumerable<Payment> GetAllPayments()
    { 
        return _dbContext.Payments
            .Include(a=> a.PaymentType)
            .Include(a=>a.Reservation)
            .Select(a=> new Payment()
            {
                Amount = a.Amount,
                PaymentDate = a.PaymentDate,
                PaymentType = new PaymentType()
                {
                    PaymentMethod = a.PaymentType.PaymentMethod
                },
                Reservation = new Reservation()
                {
                    ReservationNumber = a.Reservation.ReservationNumber
                }
            })
            .ToList();
    }
}