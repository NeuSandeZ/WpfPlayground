using System.Collections;
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

    public async Task<IEnumerable<Reservation>>GetAllReservations()
    {
        return await _dbContext.Reservations
            .Include(a => a.Guest)
            .Include(a => a.Room)
            .Include(a => a.ReservationStatus)
            .Select(r => new Reservation
            {
                Id = r.Id,
                CheckInDate = r.CheckInDate,
                CheckOutDate = r.CheckOutDate,
                TotalCost = r.TotalCost,
                Guest = new Guest
                {
                    FirstName = r.Guest.FirstName,
                    LastName = r.Guest.LastName,
                    PhoneNumber = r.Guest.PhoneNumber
                },
                Room = new Room
                {
                    FloorNumber = r.Room.FloorNumber,
                    RoomNumber = r.Room.RoomNumber
                },
                ReservationStatus = new ReservationStatus
                {
                    Status = r.ReservationStatus!.Status
                },
                ReservationNumber = r.ReservationNumber
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task CreateReservation(Reservation reservation)
    {
        var room = _dbContext.Rooms.FirstOrDefault(a => a.Id == reservation.RoomId);
        room.RoomStatusId = 3;
        _dbContext.Update(room);
        await _dbContext.AddAsync(reservation);
        await _dbContext.SaveChangesAsync();
    }

    public IEnumerable<Room> GetAllRoomsWithRoomStatus(DateTime checkInDate, DateTime checkOutDate)
    {
        var allRooms = _dbContext.Rooms.AsQueryable();

        var roomsWithReservations = _dbContext.Reservations
            .Where(r =>
                (r.CheckInDate < checkOutDate && r.CheckOutDate > checkInDate)
                || (r.CheckInDate >= checkInDate && r.CheckInDate < checkOutDate)
                || (r.CheckOutDate > checkInDate && r.CheckOutDate <= checkOutDate))
            .Select(r => r.RoomId)
            .Distinct();

        var availableRooms = allRooms.Where(room => !roomsWithReservations.Contains(room.Id));

        var availableRoomsDto = availableRooms.Include(a=> a.RoomPromotions)
            .Select(src => new Room
            {
                Id = src.Id,
                RoomNumber = src.RoomNumber,
                FloorNumber = src.FloorNumber,
                RoomStatusId = src.RoomStatusId,
                PricePerNight = src.PricePerNight,
                RoomPromotions = new RoomPromotions()
                {
                    DiscountAmount = src.RoomPromotions.DiscountAmount
                }
            })
            .AsNoTracking()
            .ToList();

        return availableRoomsDto;
    }
}