using Hotel.Application.DTOS.ReservationListingDto;

namespace Hotel.Application.Services.Interfaces;

public interface IReservationListingService
{
    IEnumerable<ReservationDto> GetAllReservations();

    Task CreateReservation(AddReservationDto reservationDto);

    IEnumerable<AvailableRoomsDto> GetAllRoomsWithRoomStatus();
}