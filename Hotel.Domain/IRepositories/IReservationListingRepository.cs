using Hotel.Domain.Entities;

namespace Hotel.Domain.IRepositories;

public interface IReservationListingRepository
{
    Task<IEnumerable<Reservation>> GetAllReservations();

    Task CreateReservation(Reservation reservation);

    IEnumerable<Room> GetAllRoomsWithRoomStatus();
}