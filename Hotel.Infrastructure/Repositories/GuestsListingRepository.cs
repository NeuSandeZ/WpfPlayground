using Hotel.Domain.Entities;
using Hotel.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure.Repositories;

public class GuestsListingRepository : IGuestsListingsRepository
{
    private readonly HotelDbContext _dbContext;

    public GuestsListingRepository(HotelDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Guest>> GetAllGuests()
    {
        return await _dbContext.Guests
            .Include(a => a.Address)
            .Select(a => new Guest
            {
                FirstName = a.FirstName,
                LastName = a.LastName,
                Email = a.Email,
                PhoneNumber = a.PhoneNumber,
                Address = new Address
                {
                    City = a.Address.City,
                    Street = a.Address.Street,
                    PostalCode = a.Address.PostalCode
                }
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public Guest GetOneGuest(string email)
    {
        return _dbContext.Guests
            .Include(a => a.Address)
            .Select(a => new Guest
            {
                FirstName = a.FirstName,
                LastName = a.LastName,
                Email = a.Email,
                PhoneNumber = a.PhoneNumber,
                Address = new Address
                {
                    City = a.Address.City,
                    Street = a.Address.Street,
                    PostalCode = a.Address.PostalCode
                }
            })
            .AsNoTracking()
            .FirstOrDefault(e => e.Email == email);
    }

    public async Task CreateGuest(Guest reservation)
    {
        await _dbContext.AddAsync(reservation);
        await _dbContext.SaveChangesAsync();
    }

    public async Task EditGuest(Guest guest)
    {
        var guestFromDb = await _dbContext.Guests
            .Include(a => a.Address)
            .FirstOrDefaultAsync(a => a.Email == guest.Email);

        guestFromDb.FirstName = guest.FirstName;
        guestFromDb.LastName = guest.LastName;
        guestFromDb.Email = guest.Email;
        guestFromDb.PhoneNumber = guest.PhoneNumber;
        guestFromDb.Address.City = guest.Address.City;
        guestFromDb.Address.Street = guest.Address.Street;
        guestFromDb.Address.PostalCode = guest.Address.PostalCode;

        _dbContext.Update(guestFromDb);
        await _dbContext.SaveChangesAsync();
    }
}