using Hotel.Application.DTOS.ReservationListingDto;

namespace Hotel.Application.Services.Interfaces;

public interface IReservationListingService
{
    Task<IEnumerable<ReservationDto>> GetAllReservations();

    Task CreateReservation(AddReservationDto reservationDto);

    IEnumerable<AvailableRoomsDto> GetAllRoomsWithRoomStatus();
}