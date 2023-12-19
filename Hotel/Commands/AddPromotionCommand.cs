using System;
using System.Windows;
using Hotel.Application.DTOS.RoomsListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.MVVM.ViewModels;

namespace Hotel.Commands;

public class AddPromotionCommand : BaseCommand
{
    private readonly IRoomListingService _roomListingService;
    private readonly RoomsViewModel _roomsViewModel;

    public AddPromotionCommand(RoomsViewModel roomsViewModel, IRoomListingService roomListingService)
    {
        _roomsViewModel = roomsViewModel;
        _roomListingService = roomListingService;
    }

    public override void Execute(object? parameter)
    {
        try
        {
            var promotionDto = new RoomPromotionDto
            {
                RoomId = _roomsViewModel.SelectedRoom.Id,
                DiscountAmount = _roomsViewModel.DiscountAmount
            };
            if (_roomsViewModel.SelectedRoom.RoomPromotion is null)
                _roomListingService.AddPromotion(promotionDto);
            else
                _roomListingService.EditPromotion(promotionDto);
        }
        catch (Exception e)
        {
            MessageBox.Show("You have to choose promotion for discount!");
        }
    }
}