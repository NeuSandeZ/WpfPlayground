using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Hotel.Application.ReservationListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.Commands;
using Hotel.Domain.Entities;
using Hotel.Infrastructure;
using Hotel.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hotel.MVVM.ViewModels;

public class AddReservationViewModel : ViewModelBase
{
    private readonly IReservationListingService _reservationListingService;
    
    private DateTime _checkInDate;
    public DateTime CheckInDate
    {
        get => _checkInDate;
        set
        {
            _checkInDate = value;
            OnPropertyChanged(nameof(CheckInDate));
        }
    }

    private DateTime _checkOutDate;
    public DateTime CheckOutDate
    {
        get { return _checkOutDate; }
        set
        {
            _checkOutDate = value;
            OnPropertyChanged(nameof(CheckOutDate));
        }
    }

    private string _firstName;

    public string FirstName
    {
        get { return _firstName; }
        set
        {
            _firstName = value;
            OnPropertyChanged(nameof(FirstName));
        }
    }

    private string _lastName;

    public string LastName
    {
        get { return _lastName; }
        set
        {
            _lastName = value;
            OnPropertyChanged(nameof(LastName));
        }
    }

    private string _totalCost;

    public string TotalCost
    {
        get
        {
            return _totalCost;
            // CheckOutDate.Subtract(CheckInDate).TotalDays.ToString();
        }
        set
        {
            _totalCost = value;
            OnPropertyChanged(nameof(CheckOutDate));
        }
    }
    
    private int _selectedRoomId;

    public int SelectedRoomId
    {
        get { return _selectedRoomId; }
        set
        {
            _selectedRoomId = value;
            OnPropertyChanged(nameof(SelectedRoomId));
        }
    }
    
    public IQueryable<AvailableRoomsDto> AvailableRooms { get; set; }

    public AddReservationViewModel(INavigator navigator,IReservationListingService reservationListingService)
    {
        _reservationListingService = reservationListingService;
        
        AddReservationCommand = new AddReservationCommand(navigator, this, _reservationListingService );
        CloseModalCommand = new CloseModalCommand(navigator);
        
        GetAvailableRooms();
    }

    public ICommand AddReservationCommand { get; }
    public ICommand CloseModalCommand { get; }

    private void GetAvailableRooms()
    {
        AvailableRooms = _reservationListingService.GetAllRoomsWithRoomStatus().AsQueryable();
    }
}
