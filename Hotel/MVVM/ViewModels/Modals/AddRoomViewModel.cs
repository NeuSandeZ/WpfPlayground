using System.Linq;
using System.Windows.Input;
using Hotel.Application.DTOS.RoomsListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.Commands;
using Hotel.MVVM.Views;
using Hotel.Services.Interfaces;

namespace Hotel.MVVM.ViewModels.Modals;

public class AddRoomViewModel : ViewModelBase
{
    private readonly IRoomListingService _roomListingService;

    public AddRoomViewModel(INavigator navigator, IRoomListingService roomListingService)
    {
        _roomListingService = roomListingService;
        CloseModalCommand = new CloseModalCommand(navigator);
        AddRoomCommand = new AddRoomCommand(navigator,roomListingService, this);

        GetAllRoomTypes();
    }

    private string _roomNumber;

    public string RoomNumber
    {
        get { return _roomNumber; }
        set
        {
            _roomNumber = value;
            OnPropertyChanged(nameof(RoomNumber));
        }
    }

    private string _floorNumber;

    public string FloorNumber
    {
        get { return _floorNumber; }
        set
        {
            _floorNumber = value;
            OnPropertyChanged(nameof(FloorNumber));
        }
    }

    private string _pricePerNight;

    public string PricePerNight
    {
        get { return _pricePerNight; }
        set
        {
            _pricePerNight = value;
            OnPropertyChanged(nameof(PricePerNight));
        }
    }

    private string _roomType;

    public string RoomType
    {
        get { return _roomType; }
        set
        {
            _roomType = value;
            OnPropertyChanged(nameof(RoomType));
        }
    }

    private int _selectedRoomTypeId;

    public int SelectedRoomTypeId
    {
        get { return _selectedRoomTypeId; }
        set
        {
            _selectedRoomTypeId = value;
            RoomType = null;
            OnPropertyChanged(nameof(SelectedRoomTypeId));
        }
    }

    public ICommand CloseModalCommand { get; set; }
    public ICommand AddRoomCommand { get; set; }
    
    public IQueryable<RoomTypeDto> RoomTypeDtos { get; set; }

    public void GetAllRoomTypes()
    {
        RoomTypeDtos = _roomListingService.GetRoomTypes().AsQueryable();
    }
}