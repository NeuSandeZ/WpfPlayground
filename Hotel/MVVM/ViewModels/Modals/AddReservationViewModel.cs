using System;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using Hotel.Application.DTOS.GuestsListingDto;
using Hotel.Application.DTOS.ReservationListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.Commands;
using Hotel.Services.Interfaces;
using Hotel.Stores;

namespace Hotel.MVVM.ViewModels.Modals;

//TODO block UI when user is choosing GUEST, also have to trigger IsChecked for specific view when user switches view using messenger for better UX
public class AddReservationViewModel : ViewModelBaseWithINotify, IRecipient<GuestDto>
{
    private readonly MessengerCurrentViewStorage _messengerCurrentViewStorage;
    private readonly IReservationListingService _reservationListingService;

    private IQueryable<AvailableRoomsDto> _availableRooms;

    private DateTime _checkInDate = DateTime.Now.Date;

    private DateTime _checkOutDate = DateTime.Now.Date;

    private string _city;

    private string _email;

    private string _firstName;

    private string _lastName;

    private string _phoneNumber;

    private string _postalCode;

    private int _selectedGuestId;

    private int _selectedRoomId;

    private string _street;

    private string _totalCost;


    public AddReservationViewModel(INavigator navigator, IReservationListingService reservationListingService,
        MessengerCurrentViewStorage messengerCurrentViewStorage)
    {
        _reservationListingService = reservationListingService;
        _messengerCurrentViewStorage = messengerCurrentViewStorage;

        AddReservationCommand = new AddReservationCommand(navigator, this, _reservationListingService);
        CloseModalCommand = new CloseModalCommand(navigator);
        ChooseGuestCommand = new ChooseGuestCommand("OpenGuests", _messengerCurrentViewStorage, this);
    }

    public DateTime CheckInDate
    {
        get => _checkInDate;
        set
        {
            _checkInDate = value;
            OnPropertyChanged();

            ClearErrors(nameof(CheckInDate));
            ClearErrors(nameof(CheckOutDate));

            if (CheckOutDate < CheckInDate)
            {
                AddError("Check in date cannot be after the check out date.", nameof(CheckInDate));
                OnErrorsChanged(nameof(CheckInDate));
            }
        }
    }

    public DateTime CheckOutDate
    {
        get => _checkOutDate;
        set
        {
            _checkOutDate = value;
            OnPropertyChanged();

            ClearErrors(nameof(CheckOutDate));
            ClearErrors(nameof(CheckInDate));

            if (CheckOutDate < CheckInDate)
            {
                AddError("Check out date cannot be before the check in date.", nameof(CheckOutDate));
                OnErrorsChanged(nameof(CheckOutDate));
            }

            CalculateTotalCost();
            GetAvailableRooms(_checkInDate, _checkOutDate);
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

    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged();
        }
    }


    public string PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            _phoneNumber = value;
            OnPropertyChanged();
        }
    }

    public string City
    {
        get => _city;
        set
        {
            _city = value;
            OnPropertyChanged();
        }
    }


    public string Street
    {
        get => _street;
        set
        {
            _street = value;
            OnPropertyChanged();
        }
    }


    public string PostalCode
    {
        get => _postalCode;
        set
        {
            _postalCode = value;
            OnPropertyChanged();
        }
    }

    public string TotalCost
    {
        get => _totalCost + "$";
        set
        {
            _totalCost = value;
            OnPropertyChanged();
        }
    }

    public int SelectedRoomId
    {
        get => _selectedRoomId;
        set
        {
            _selectedRoomId = value;
            CalculateTotalCost();
            OnPropertyChanged();
        }
    }

    public int SelectedGuestId
    {
        get => _selectedGuestId;
        set
        {
            _selectedGuestId = value;
            OnPropertyChanged();
        }
    }


    public IQueryable<AvailableRoomsDto> AvailableRooms
    {
        get => _availableRooms;
        set
        {
            _availableRooms = value;
            OnPropertyChanged();
        }
    }

    public ICommand AddReservationCommand { get; }
    public ICommand ChooseGuestCommand { get; }
    public ICommand CloseModalCommand { get; }

    public void Receive(GuestDto message)
    {
        FirstName = message.FirstName;
        LastName = message.LastName;
        Email = message.Email;
        PhoneNumber = message.PhoneNumber;
        City = message.City;
        Street = message.Street;
        PostalCode = message.PostalCode;
        SelectedGuestId = message.GuestId;

        WeakReferenceMessenger.Default.Send<string>("CloseGuests");
        WeakReferenceMessenger.Default.UnregisterAll(this);
    }


    private void GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate)
    {
        AvailableRooms = _reservationListingService.GetAllRoomsWithRoomStatus(checkInDate, checkOutDate).AsQueryable();
    }

    private void CalculateTotalCost()
    {
        if (AvailableRooms == null && SelectedRoomId < 0) return;

        var selectedRoom = AvailableRooms?.FirstOrDefault(room => room.RoomId == SelectedRoomId);
        if (selectedRoom != null)
        {
            var span = CheckOutDate.Subtract(CheckInDate).Days;
            double totalPrice = span * selectedRoom.PricePerNight;
            if (selectedRoom.DiscountAmount > 0)
            {
                var discount = totalPrice * (selectedRoom.DiscountAmount / 100.0);
                totalPrice -= discount;
            }

            _totalCost = totalPrice.ToString();
            OnPropertyChanged(nameof(TotalCost));
        }
    }

    public void RegisterAddReservationViewModel()
    {
        WeakReferenceMessenger.Default.Register(this);
    }
}