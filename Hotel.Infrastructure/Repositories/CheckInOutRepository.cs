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
    
    public IEnumerable<Reservation> GetAllReservationNumbers()
    {
        //TODO add filtering for reservation with check in date as today
        return _dbContext.Reservations.Select(a => new Reservation()
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
            }).AsNoTracking()
            .ToList();
    }

    public void CreateCheckIn(CheckIns checkIn)
    {
        _dbContext.Add(checkIn);
        _dbContext.SaveChanges();
    }

    public IEnumerable<CheckIns> GetAllCheckIns()
    {
        return _dbContext.CheckIns.Select(a => new CheckIns()
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
            }).AsTracking()
            .ToList();
    }

    public void CheckOut(CheckOuts checkOuts)
    {
        _dbContext.Add(checkOuts);
        _dbContext.SaveChanges();
    }

    //TODO hardcode some date that matches my records in DB, also have to create some property to decrease value when checkIn is performed
    public int GetTodaysCheckIns()
    {
        return _dbContext.Reservations.Count(a => a.CheckInDate.Date == DateTime.Now.Date);
    }

    public int GetTodaysCheckOuts()
    {
        return _dbContext.Reservations.Count(a => a.CheckOutDate.Date == DateTime.Now.Date);
    }
}
