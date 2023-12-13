﻿using System.Collections;
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
                    Status = r.ReservationStatus.Status
                },
                ReservationNumber = r.ReservationNumber
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task CreateReservation(Reservation reservation)
    {
        var room = _dbContext.Rooms.FirstOrDefault(a => a.Id == reservation.RoomId);
        //TODO weird ID on DB
        room.RoomStatusId = 1002;
        _dbContext.Update(room);
        await _dbContext.AddAsync(reservation);
        await _dbContext.SaveChangesAsync();
    }

    public IEnumerable<Room> GetAllRoomsWithRoomStatus()
    {
        return _dbContext.Rooms
            .Select(src => new Room
            {
                Id = src.Id,
                RoomNumber = src.RoomNumber,
                FloorNumber = src.FloorNumber,
                RoomStatusId = src.RoomStatusId,
                PricePerNight = src.PricePerNight
            })
            .ToList();
    }
}