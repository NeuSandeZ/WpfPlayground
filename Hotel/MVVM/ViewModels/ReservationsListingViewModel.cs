using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using Hotel.Application.DTOS.ReservationListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.Commands;
using Hotel.Factories;
using Hotel.Services.Interfaces;
using Hotel.Stores;

namespace Hotel.MVVM.ViewModels;

public class ReservationsListingViewModel : SortingAndFilteringViewModel
{
    private readonly MessengerCurrentViewStorage _messengerCurrentViewStorage;
    private readonly INavigator _navigator;
    private readonly IReservationListingService _reservationListingService;

    private ObservableCollection<ReservationDto> _reservations;

    private ReservationDto _selectedReservation;

    public ReservationsListingViewModel(INavigator navigator,
        IViewModelFactory viewModelFactory,
        IReservationListingService reservationListingService,
        MessengerCurrentViewStorage messengerCurrentViewStorage)
    {
        _navigator = navigator;
        _reservationListingService = reservationListingService;
        _messengerCurrentViewStorage = messengerCurrentViewStorage;

        //TODO Sending query to database everytime i regrab that view is a bad idea, prolly have to figure out how to load it asynchronously and cache it
        GetAllReservations();
        
        SortComboBoxList = new ObservableCollection<string>(GetComboBoxSortList());
        FilterComboBoxList = new ObservableCollection<string>(GetComboBoxFilterList());

        OpenModal = new OpenModalCommand(navigator, viewModelFactory, () => ViewType.AddCrud);
    }

    public bool IsTemporaryViewModelOpened => _messengerCurrentViewStorage.IsTemporaryViewModelOpened;

    public ICommand OpenModal { get; }

    public ObservableCollection<ReservationDto> Reservations
    {
        get => _reservations;
        set
        {
            _reservations = value;
            OnPropertyChanged();
        }
    }

    public ReservationDto SelectedReservation
    {
        get => _selectedReservation;
        set
        {
            _selectedReservation = value;
            WeakReferenceMessenger.Default.Send(SelectedReservation);
            OnPropertyChanged();
        }
    }

    public async Task GetAllReservations()
    {
        var reservationDtos = await _reservationListingService.GetAllReservations();
        Reservations = new ObservableCollection<ReservationDto>(reservationDtos);
    }
    
    
    private const string CHECKOUT_SORT_VALUE = "Check out";
    private const string CHECKIN_SORT_VALUE = "Check in";
    private const string GUESTFULLNAME_SORT_DESC_VALUE = "Guest fullname";
    
    public override List<string> GetComboBoxSortList()
    {
        return new List<string>()
        {
            CHECKOUT_SORT_VALUE, CHECKIN_SORT_VALUE, GUESTFULLNAME_SORT_DESC_VALUE
        };
    }
    
    public override void Sort()
    {
        if (SortField == CHECKOUT_SORT_VALUE)
        {
            Reservations = new ObservableCollection<ReservationDto>(Reservations.OrderBy(x => x.CheckOutDate));
        }
        if (SortField == CHECKIN_SORT_VALUE)
        {
            Reservations = new ObservableCollection<ReservationDto>(Reservations.OrderBy(x => x.CheckInDate));
        }
        if(SortField == GUESTFULLNAME_SORT_DESC_VALUE)
        {
            Reservations = new ObservableCollection<ReservationDto>(Reservations.OrderBy(x => x.GuestFullName));
        }
    }

    private const string GUESTFULLNAME_SORT_VALUE = "Guest fullname";
    
    public override List<string> GetComboBoxFilterList()
    {
        return new List<string>()
        {
            GUESTFULLNAME_SORT_VALUE
        };
    }

    public override void Filter()
    {
        if (FilterField == GUESTFULLNAME_SORT_VALUE)
        {
            Reservations = new ObservableCollection<ReservationDto>(
                Reservations.Where(x => x.GuestFullName.Contains(FilterTexBoxInputField, StringComparison.OrdinalIgnoreCase)));
        }
    }
}