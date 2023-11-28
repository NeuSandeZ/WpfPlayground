using AutoMapper;
using Hotel.Application.DTOS.RoomsListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.Domain.IRepositories;

namespace Hotel.Application.Services;

public class RoomListingService : IRoomListingService
{
    private readonly IMapper _mapper;
    private readonly IRoomListingRepository _roomListingRepository;

    public RoomListingService(IMapper mapper, IRoomListingRepository roomListingRepository)
    {
        _mapper = mapper;
        _roomListingRepository = roomListingRepository;
    }

    public async Task<IEnumerable<RoomsListingDto>> GetAllRooms()
    {
        var allRooms = await _roomListingRepository.GetAllRooms();
        return _mapper.Map<IEnumerable<RoomsListingDto>>(allRooms);
    }
}