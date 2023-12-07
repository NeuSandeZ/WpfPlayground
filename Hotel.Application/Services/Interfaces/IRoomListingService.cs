using Hotel.Application.DTOS.RoomsListingDto;
using Hotel.Domain.Entities;

namespace Hotel.Application.Services.Interfaces;

public interface IRoomListingService
{
    Task<IEnumerable<RoomsListingDto>> GetAllRooms();

    Task CreateRoom(RoomsListingDto roomsListingDto);

    IEnumerable<RoomTypeDto> GetRoomTypes();
    
    void AddPromotion(RoomPromotionDto promotionDto);
    void EditPromotion(RoomPromotionDto promotionDto);
}