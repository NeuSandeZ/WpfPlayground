using Hotel.Domain.Entities;

namespace Hotel.Domain.IRepositories;

public interface IRoomListingRepository
{
    Task<IEnumerable<Room>> GetAllRooms();
}