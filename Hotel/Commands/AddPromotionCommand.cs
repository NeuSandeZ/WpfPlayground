using Hotel.Application.DTOS.RoomsListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.MVVM.ViewModels;
using Hotel.Services.Interfaces;

namespace Hotel.Commands;

public class AddPromotionCommand : BaseCommand
{
    private readonly RoomsViewModel _roomsViewModel;
    private readonly IRoomListingService _roomListingService;

    public AddPromotionCommand( RoomsViewModel roomsViewModel, IRoomListingService roomListingService)
    {
        _roomsViewModel = roomsViewModel;
        _roomListingService = roomListingService;
    }
    
    public override void Execute(object? parameter)
    {
        var promotionDto = new RoomPromotionDto()
        {
            RoomId = _roomsViewModel.SelectedRoom.Id,
            DiscountAmount = _roomsViewModel.DiscountAmount
        };
        
        if ( _roomsViewModel.SelectedRoom.RoomPromotion is null)
        {
            _roomListingService.AddPromotion(promotionDto);
        }
        else
        {
            _roomListingService.EditPromotion(promotionDto);
        }
    }
}