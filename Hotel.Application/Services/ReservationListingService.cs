using Hotel.Application.Services.Interfaces;
using Hotel.Domain.Entities;
using Hotel.Domain.IRepositories;

namespace Hotel.Application.Services;

public class ReservationListingService : IReservationListingService
{
    private readonly IReservationListingRepository _reservationListingRepository;

    public ReservationListingService(IReservationListingRepository reservationListingRepository)
    {
        _reservationListingRepository = reservationListingRepository;
    }
    public async Task<IEnumerable<Reservation>> GetAll()
    {
       return await _reservationListingRepository.GetAll();
    }
}