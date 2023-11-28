using Hotel.Application.DTOS.RoomsListingDto;

namespace Hotel.Application.Services.Interfaces;

public interface IRoomListingService
{
    Task<IEnumerable<RoomsListingDto>> GetAllRooms();
}