using Hotel.Application.DTOS.GuestsListingDto;

namespace Hotel.Application.Services.Interfaces;

public interface IGuestsListingService
{
    Task<IEnumerable<GuestDto>> GetAllGuests();
    GuestDto GetOneGuest(string email);
    Task CreateGuest(GuestDto reservationDto);
    Task EditGuest(GuestDto editedGuest);
}