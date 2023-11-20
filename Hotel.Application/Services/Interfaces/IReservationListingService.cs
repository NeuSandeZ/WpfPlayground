using Hotel.Application.ReservationListingDto;
using Hotel.Domain.Entities;

namespace Hotel.Application.Services.Interfaces;

public interface IReservationListingService
{
    Task<IEnumerable<ReservationDto>> GetAllReservations();

    Task CreateReservation(AddReservationDto reservationDto);

    IEnumerable<AvailableRoomsDto> GetAllRoomsWithRoomStatus();
}