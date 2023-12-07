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
                Id = a.Id,
                RoomNumber = a.RoomNumber,
                FloorNumber = a.FloorNumber,
                PricePerNight = a.PricePerNight,
                RoomPromotions = new RoomPromotions()
                {
                    DiscountAmount = a.RoomPromotions.DiscountAmount
                },
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

    public async Task CreateRoom(Room room)
    {
        _hotelDbContext.Rooms.Add(room);
        await _hotelDbContext.SaveChangesAsync();
    }

    public IEnumerable<RoomType> GetRoomTypes()
    {
        return _hotelDbContext.RoomTypes.AsNoTracking().ToList();
    }

    public void AddPromotion(RoomPromotions roomPromotions)
    {
        _hotelDbContext.Add(roomPromotions);
        _hotelDbContext.SaveChanges();
    }

    public void EditPromotion(RoomPromotions roomPromotions)
    {
        var promotions = _hotelDbContext.RoomPromotions.FirstOrDefault(a => a.RoomId == roomPromotions.RoomId);
        if (promotions != null)
        {
            promotions.DiscountAmount = roomPromotions.DiscountAmount;
            _hotelDbContext.Update(promotions);
        }
        _hotelDbContext.SaveChanges();
    }
}