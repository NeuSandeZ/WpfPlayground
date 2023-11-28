using Hotel.Domain.Entities;

namespace Hotel.Domain.IRepositories;

public interface IGuestsListingsRepository
{
    Task<IEnumerable<Guest>> GetAllGuests();
    Guest GetOneGuest(string email);
    Task CreateGuest(Guest guest);
    Task EditGuest(Guest guest);
}