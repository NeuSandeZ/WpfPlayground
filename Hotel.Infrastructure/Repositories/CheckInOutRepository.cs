using Hotel.Application.DTOS.CheckInsOutsDto;
using Hotel.Domain.Entities;
using Hotel.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure.Repositories;

public class CheckInOutRepository : ICheckInOutRepository
{
    private readonly HotelDbContext _dbContext;
    
    public CheckInOutRepository(HotelDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<Reservation>> GetAllReservationNumbers()
    {
        //TODO add filtering for reservation with check in date as today
        return await _dbContext.Reservations
            .AsNoTracking()
            .Where(a=> a.IsCheckedIn == false)
            .Select(a => new Reservation()
            {
                Id = a.Id,
                ReservationNumber = a.ReservationNumber,
                Room = new Room()
                {
                    FloorNumber = a.Room.FloorNumber,
                    RoomNumber = a.Room.RoomNumber
                },
                RoomId = a.RoomId,
                GuestId = a.GuestId
            })
            .ToListAsync();
    }

    public void CreateCheckIn(CheckIns checkIn)
    {
        var reservation = _dbContext.Reservations.FirstOrDefault(a => a.Id == checkIn.ReservationId);
        if (reservation is not null)
        {
            reservation.IsCheckedIn = true;
            _dbContext.Update(reservation);
        }
        _dbContext.Add(checkIn);
        _dbContext.SaveChanges();
    }

    public async Task<IEnumerable<CheckIns>> GetAllCheckIns()
    {
        return await _dbContext.CheckIns.AsNoTracking().Select(a => new CheckIns()
            {
                Id = a.Id,
                CheckInDate = a.CheckInDate,
                CheckOuts = new CheckOuts()
                {
                    CheckOutDate = a.CheckOuts.CheckOutDate
                },
                Guest = new Guest()
                {
                    FirstName = a.Guest.FirstName,
                    LastName = a.Guest.LastName
                },
                Room = new Room()
                {
                    FloorNumber = a.Room.FloorNumber,
                    RoomNumber = a.Room.RoomNumber,
                },
                Reservation = new Reservation()
                {
                    ReservationNumber = a.Reservation.ReservationNumber
                }
            })
            .ToListAsync();
    }

    public void CheckOut(CheckOuts checkOuts)
    {
        var checkIn = _dbContext.CheckIns.FirstOrDefault(a => a.Id == checkOuts.CheckInsId);
        var reservation = _dbContext.Reservations.FirstOrDefault(a => a.Id == checkIn.ReservationId);
        if (reservation is not null)
        {
            reservation.IsCheckedOut = true;
            _dbContext.Update(reservation);
        }
        _dbContext.Add(checkOuts);
        _dbContext.SaveChanges();
    }

    //TODO hardcode some date that matches my records in DB, also have to create some property to decrease value when checkIn is performed
    public async Task<int> GetTodaysCheckIns()
    {
        return await _dbContext.Reservations.AsNoTracking().CountAsync(a => a.CheckInDate.Date == DateTime.Now.Date && a.IsCheckedIn == false);
    }

    public async Task<int> GetTodaysCheckOuts()
    {
        return await _dbContext.Reservations.AsNoTracking().CountAsync(a => a.CheckOutDate.Date == DateTime.Now.Date && a.IsCheckedOut == false);
    }
}
