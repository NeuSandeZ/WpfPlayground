using Hotel.Domain.Entities;
using Hotel.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure.Repositories;

public class RoomListingRepository : IRoomListingRepository
{
    private readonly HotelDbContext _hotelDbContext;

    public RoomListingRepository(HotelDbContext hotelDbContext)
    {
        _hotelDbContext = hotelDbContext;
    }

    public async Task<IEnumerable<Room>> GetAllRooms()
    {
        return await _hotelDbContext.Rooms
            .Include(a => a.RoomType)
            .Include(a => a.RoomStatus)
            .Select(a => new Room
            {
                RoomNumber = a.RoomNumber,
                FloorNumber = a.FloorNumber,
                PricePerNight = a.PricePerNight,
                RoomStatus = new RoomStatus
                {
                    CurrentState = a.RoomStatus.CurrentState
                },
                RoomType = new RoomType
                {
                    Type = a.RoomType.Type
                }
            })
            .AsNoTracking()
            .ToListAsync();
    }
}