using AutoMapper;
using Hotel.Application.DTOS.RoomsListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.Domain.Entities;
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

    public async Task CreateRoom(RoomsListingDto roomsListingDto)
    {
        var room = new Room
        {
            RoomNumber = int.Parse(roomsListingDto.RoomNumber),
            FloorNumber = int.Parse(roomsListingDto.FloorNumber),
            PricePerNight = int.Parse(roomsListingDto.PricePerNight)
        };

        if (roomsListingDto.SelectedRoomTypeId != 0)
        {
            room.RoomTypeId = roomsListingDto.SelectedRoomTypeId;
        }
        else
        {
            room.RoomType = new RoomType()
            {
                Type = roomsListingDto.RoomType
            };
        }
        await _roomListingRepository.CreateRoom(room);
    }

    public IEnumerable<RoomTypeDto> GetRoomTypes()
    {
        var roomTypes = _roomListingRepository.GetRoomTypes();
        return _mapper.Map<IEnumerable<RoomTypeDto>>(roomTypes);
    }

    public void AddPromotion(RoomPromotionDto promotionDto)
    {
        var room = new RoomPromotions()
        {
            DiscountAmount = promotionDto.DiscountAmount,
            RoomId = promotionDto.RoomId
        };

        _roomListingRepository.AddPromotion(room);
    }

    public void EditPromotion(RoomPromotionDto promotionDto)
    {
        var room = new RoomPromotions()
        {
            DiscountAmount = promotionDto.DiscountAmount,
            RoomId = promotionDto.RoomId
        };

        _roomListingRepository.EditPromotion(room);
    }
}