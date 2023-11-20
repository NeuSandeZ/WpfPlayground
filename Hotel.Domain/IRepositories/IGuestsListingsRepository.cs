using Hotel.Domain.Entities;

namespace Hotel.Domain.IRepositories;

public interface IGuestsListingsRepository
{
    Task<IEnumerable<Guest>> GetAllGuests();
    Task CreateGuest(Guest reservation);
}