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
    public async Task<IEnumerable<Reservation>> GetAll()
    {
        return await _dbContext.Reservations.ToListAsync();
    }
}