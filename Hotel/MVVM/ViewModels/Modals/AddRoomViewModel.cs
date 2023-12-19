using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Hotel.Application.DTOS.RoomsListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.Commands;
using Hotel.Services.Interfaces;

namespace Hotel.MVVM.ViewModels.Modals;

public class AddRoomViewModel : ViewModelBaseWithINotify
{
    private readonly IRoomListingService _roomListingService;

    private string _floorNumber;

    private string _pricePerNight;

    private string _roomNumber;

    private string _roomType;

    private int _selectedRoomTypeId;

    public AddRoomViewModel(INavigator navigator, IRoomListingService roomListingService)
    {
        _roomListingService = roomListingService;
        CloseModalCommand = new CloseModalCommand(navigator);
        AddRoomCommand = new AddRoomCommand(navigator, roomListingService, this);

        GetAllRoomTypes();
    }

    public string RoomNumber
    {
        get => _roomNumber;
        set
        {
            _roomNumber = value;
            OnPropertyChanged();

            ClearErrors(nameof(RoomNumber));
            if (string.IsNullOrWhiteSpace(value))
            {
                AddError("Room number cannot be empty or null.", nameof(RoomNumber));
                OnErrorsChanged(nameof(RoomNumber));
            }
            else if (!int.TryParse(value, out var roomNumber) || roomNumber <= 0)
            {
                AddError("Invalid room number. It should be a positive integer.", nameof(RoomNumber));
                OnErrorsChanged(nameof(RoomNumber));
            }
        }
    }

    public string FloorNumber
    {
        get => _floorNumber;
        set
        {
            _floorNumber = value;
            OnPropertyChanged();

            ClearErrors(nameof(FloorNumber));
            if (!(value.All(c => char.IsDigit(c) || c == '-') &&
                  (int.Parse(value) >= 0 || value.IndexOf('-') == 0)))
            {
                AddError("Invalid floor number. It should be an integer or a negative integer.", nameof(FloorNumber));
                OnErrorsChanged(nameof(FloorNumber));
            }
        }
    }

    public string PricePerNight
    {
        get => _pricePerNight;
        set
        {
            _pricePerNight = value;
            OnPropertyChanged();

            ClearErrors(nameof(PricePerNight));
            if (!Regex.IsMatch(value, @"^\d+(\.\d+)?$"))
            {
                AddError("Invalid floor number. It should be an integer or a negative integer.", nameof(PricePerNight));
                OnErrorsChanged(nameof(PricePerNight));
            }
        }
    }

    public string RoomType
    {
        get => _roomType;
        set
        {
            _roomType = value;
            OnPropertyChanged();

            ClearErrors(nameof(RoomType));
            if (string.IsNullOrWhiteSpace(value))
            {
                AddError("Room type cannot be empty or null.", nameof(RoomType));
                OnErrorsChanged(nameof(RoomType));
            }
            else if (!Regex.IsMatch(value, "^[a-zA-Z]+$"))
            {
                AddError("Room type must contain only letters.", nameof(RoomType));
                OnErrorsChanged(nameof(RoomType));
            }
        }
    }

    public int SelectedRoomTypeId
    {
        get => _selectedRoomTypeId;
        set
        {
            _selectedRoomTypeId = value;
            RoomType = null;
            OnPropertyChanged();
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