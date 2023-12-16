using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
public class AddReservationViewModel : ViewModelBase, IRecipient<GuestDto>, INotifyDataErrorInfo
{
    private readonly IReservationListingService _reservationListingService;
    private readonly MessengerCurrentViewStorage _messengerCurrentViewStorage;

    private DateTime _checkInDate = DateTime.Now.Date;

    private DateTime _checkOutDate = DateTime.Now.Date;
    
    private int _selectedGuestId;
    
    private int _selectedRoomId;
    
    private string _firstName;
    
    private string _lastName;
    
    private string _totalCost;
    
    private string _email;
    
    private string _city;
    
    private string _street;
    
    private string _postalCode;
    
    private string _phoneNumber;
    
    private IQueryable<AvailableRoomsDto> _availableRooms;

    
    public AddReservationViewModel(INavigator navigator, IReservationListingService reservationListingService,
        MessengerCurrentViewStorage messengerCurrentViewStorage)
    {
        _reservationListingService = reservationListingService;
        _messengerCurrentViewStorage = messengerCurrentViewStorage;

        _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();

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
            OnPropertyChanged(nameof(CheckInDate));
            
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
            OnPropertyChanged(nameof(CheckOutDate));

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
            OnPropertyChanged(nameof(FirstName));
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;
            OnPropertyChanged(nameof(LastName));
        }
    }

    public string Email
    {
        get { return _email; }
        set
        {
            _email = value;
            OnPropertyChanged(nameof(Email));
        }
    }


    public string PhoneNumber
    {
        get { return _phoneNumber; }
        set
        {
            _phoneNumber = value;
            OnPropertyChanged(nameof(PhoneNumber));
        }
    }

    public string City
    {
        get { return _city; }
        set
        {
            _city = value;
            OnPropertyChanged(nameof(City));
        }
    }


    public string Street
    {
        get { return _street; }
        set
        {
            _street = value;
            OnPropertyChanged(nameof(Street));
        }
    }


    public string PostalCode
    {
        get { return _postalCode; }
        set
        {
            _postalCode = value;
            OnPropertyChanged(nameof(PostalCode));
        }
    }

    public string TotalCost
    {
        get => _totalCost + "$";
        set
        {
            _totalCost = value;
            OnPropertyChanged(nameof(TotalCost));
        }
    }

    public int SelectedRoomId
    {
        get => _selectedRoomId;
        set
        {
            _selectedRoomId = value;
            CalculateTotalCost();
            OnPropertyChanged(nameof(SelectedRoomId));
        }
    }
    public int SelectedGuestId
    {
        get => _selectedGuestId;
        set
        {
            _selectedGuestId = value;
            OnPropertyChanged(nameof(SelectedGuestId));
        }
    }


    public IQueryable<AvailableRoomsDto>  AvailableRooms   
    {
        get { return _availableRooms; }
        set
        {
            _availableRooms = value;
            OnPropertyChanged(nameof(AvailableRooms));
        }
    }
    public ICommand AddReservationCommand { get; }
    public ICommand ChooseGuestCommand { get; }
    public ICommand CloseModalCommand { get; }


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
            var totalPrice = span * selectedRoom.PricePerNight;
            _totalCost = totalPrice.ToString();
            OnPropertyChanged(nameof(TotalCost));
        }
    }

    public void RegisterAddReservationViewModel()
    {
        WeakReferenceMessenger.Default.Register(this);
    }

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

    private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;
    public IEnumerable GetErrors(string? propertyName)
    {
        return _propertyNameToErrorsDictionary.GetValueOrDefault(propertyName, new List<string>());
    }

    private void ClearErrors(string propertyName)
    {
        _propertyNameToErrorsDictionary.Remove(propertyName);
        OnErrorsChanged(propertyName);
    }
    
    private void AddError(string errorMessage, string propertyName)
    {
        if (!_propertyNameToErrorsDictionary.ContainsKey(propertyName))
        {
            _propertyNameToErrorsDictionary.Add(propertyName, new List<string>());
        }
        _propertyNameToErrorsDictionary[propertyName].Add(errorMessage);
        OnErrorsChanged(propertyName);
    }
    
    private void OnErrorsChanged(string propertyName)
    {
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }
    
    public bool HasErrors => _propertyNameToErrorsDictionary.Any();
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
}