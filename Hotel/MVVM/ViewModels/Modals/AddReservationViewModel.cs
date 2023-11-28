using System;
using System.Linq;
using System.Windows.Input;
using Hotel.Application.DTOS.ReservationListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.Commands;
using Hotel.Services.Interfaces;

namespace Hotel.MVVM.ViewModels.Modals;

public class AddReservationViewModel : ViewModelBase
{
    private readonly IReservationListingService _reservationListingService;
    private readonly ReservationsListingViewModel _reservationsListing;

    private DateTime _checkInDate;

    private DateTime _checkOutDate;

    private string _firstName;

    private string _lastName;

    private int _selectedRoomId;

    private string _totalCost;

    public AddReservationViewModel(INavigator navigator, IReservationListingService reservationListingService)
    {
        _reservationListingService = reservationListingService;

        AddReservationCommand = new AddReservationCommand(navigator, this, _reservationListingService);
        CloseModalCommand = new CloseModalCommand(navigator);

        GetAvailableRooms();
    }

    public DateTime CheckInDate
    {
        get => _checkInDate;
        set
        {
            _checkInDate = value;
            OnPropertyChanged();
        }
    }

    public DateTime CheckOutDate
    {
        get => _checkOutDate;
        set
        {
            _checkOutDate = value;
            OnPropertyChanged();
        }
    }

    public string FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;
            OnPropertyChanged();
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;
            OnPropertyChanged();
        }
    }

    public string TotalCost
    {
        get => _totalCost;
        // CheckOutDate.Subtract(CheckInDate).TotalDays.ToString();
        set
        {
            _totalCost = value;
            OnPropertyChanged(nameof(CheckOutDate));
        }
    }

    public int SelectedRoomId
    {
        get => _selectedRoomId;
        set
        {
            _selectedRoomId = value;
            OnPropertyChanged();
        }
    }

    public IQueryable<AvailableRoomsDto> AvailableRooms { get; set; }

    public ICommand AddReservationCommand { get; }
    public ICommand CloseModalCommand { get; }

    private void GetAvailableRooms()
    {
        AvailableRooms = _reservationListingService.GetAllRoomsWithRoomStatus().AsQueryable();
    }
}