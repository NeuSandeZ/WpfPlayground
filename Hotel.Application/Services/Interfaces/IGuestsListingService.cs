using Hotel.Application.DTOS.GuestsListingDto;
using Hotel.Domain.Entities;

namespace Hotel.Application.Services.Interfaces;

public interface IGuestsListingService
{
    Task<IEnumerable<GuestDto>> GetAllGuests();
    Task CreateGuest(GuestDto reservationDto);
}