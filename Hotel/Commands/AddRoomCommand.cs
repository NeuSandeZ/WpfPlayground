using System.Windows;
using Hotel.Application.DTOS.RoomsListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.MVVM.ViewModels.Modals;
using Hotel.Services.Interfaces;

namespace Hotel.Commands;

public class AddRoomCommand : BaseCommand
{
    private readonly INavigator _navigator;
    private readonly IRoomListingService _roomListingService;
    private readonly AddRoomViewModel _roomsViewModel;

    public AddRoomCommand(INavigator navigator, IRoomListingService roomListingService, AddRoomViewModel roomsViewModel)
    {
        _navigator = navigator;
        _roomListingService = roomListingService;
        _roomsViewModel = roomsViewModel;
    }

    public override void Execute(object? parameter)
    {
        var roomDto = new RoomsListingDto
        {
            FloorNumber = _roomsViewModel.FloorNumber,
            RoomNumber = _roomsViewModel.RoomNumber,
            PricePerNight = _roomsViewModel.PricePerNight,
            RoomType = _roomsViewModel.RoomType,
            SelectedRoomTypeId = _roomsViewModel.SelectedRoomTypeId
        };

        if (roomDto is not null && !_roomsViewModel.HasErrors)
        {
            _roomListingService.CreateRoom(roomDto);
            MessageBox.Show("Guest added!");
            _navigator.Close();
        }
        else
        {
            MessageBox.Show("Fill in the template!");
        }
    }
}