using AutoMapper;
using Hotel.Application.DTOS.GuestsListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.Domain.Entities;
using Hotel.Domain.IRepositories;

namespace Hotel.Application.Services;

public class GuestsListingService : IGuestsListingService
{
    private readonly IGuestsListingsRepository _guestsListingsRepository;
    private readonly IMapper _mapper;

    public GuestsListingService(IMapper mapper, IGuestsListingsRepository guestsListingsRepository)
    {
        _mapper = mapper;
        _guestsListingsRepository = guestsListingsRepository;
    }

    public async Task<IEnumerable<GuestDto>> GetAllGuests()
    {
        var allGuests = await _guestsListingsRepository.GetAllGuests();
        return _mapper.Map<IEnumerable<GuestDto>>(allGuests);
    }

    public async Task CreateGuest(GuestDto guestDto)
    {
        var guest = _mapper.Map<Guest>(guestDto);
        await _guestsListingsRepository.CreateGuest(guest);
    }
}