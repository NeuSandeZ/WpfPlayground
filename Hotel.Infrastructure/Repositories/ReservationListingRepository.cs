using System.Collections.ObjectModel;
using Hotel.Domain.Entities;
using Hotel.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure.Repositories;

public class ReservationListingRepository : IReservationListingRepository
{
    private readonly HotelDbContext _dbContext;

    public ReservationListingRepository(HotelDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<Reservation>> GetAllReservations()
    {
        return await _dbContext.Reservations
            .Include(a=>a.Guest)
            .Include(a=>a.Room)
            .Select(r=> new Reservation
            {
                CheckInDate = r.CheckInDate,
                CheckOutDate = r.CheckOutDate,
                TotalCost = r.TotalCost,
                Guest = new Guest()
                {
                    FirstName = r.Guest.FirstName,
                    LastName = r.Guest.LastName,
                    PhoneNumber = r.Guest.PhoneNumber
                },
                Room = new Room()
                {
                    FloorNumber = r.Room.FloorNumber,
                    RoomNumber = r.Room.RoomNumber
                }
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task CreateReservation(Reservation reservation)
    {
        await _dbContext.AddAsync(reservation);
        await _dbContext.SaveChangesAsync();
    }

    public IEnumerable<Room> GetAllRoomsWithRoomStatus()
    {
        return _dbContext.Rooms
            .Select(src => new Room()
            {
                Id = src.Id,
                RoomNumber = src.RoomNumber,
                FloorNumber = src.FloorNumber,
                RoomStatusId = src.RoomStatusId,
                IsAvailable = src.IsAvailable
            }).ToList();
    }
}