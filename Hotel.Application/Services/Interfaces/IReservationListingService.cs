using Hotel.Domain.Entities;

namespace Hotel.Application.Services.Interfaces;

public interface IReservationListingService
{
    Task<IEnumerable<Reservation>> GetAll();
}