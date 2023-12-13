using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using Hotel.Application.DTOS.ReservationListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.Commands;
using Hotel.Commands.AsyncCommands;
using Hotel.Factories;
using Hotel.Services.Interfaces;
using Hotel.Stores;

namespace Hotel.MVVM.ViewModels;

public sealed class ReservationsListingViewModel : SortingAndFilteringViewModel
{
    private readonly MessengerCurrentViewStorage _messengerCurrentViewStorage;
    private readonly INavigator _navigator;
    private readonly IReservationListingService _reservationListingService;

    private ObservableCollection<ReservationDto> _reservations;

    private ReservationDto _selectedReservation;
    
    private string _reservationsFilterField = string.Empty;


    public ReservationsListingViewModel(INavigator navigator,
        IViewModelFactory viewModelFactory,
        IReservationListingService reservationListingService,
        MessengerCurrentViewStorage messengerCurrentViewStorage)
    {
        _navigator = navigator;
        _reservationListingService = reservationListingService;
        _messengerCurrentViewStorage = messengerCurrentViewStorage;

        //TODO Sending query to database everytime i regrab that view is a bad idea, prolly have to figure out how to load it asynchronously and cache it
        //TODO Finally managed to load it asynchronously but sending query to DB everytime i regrab this view is bad idea
        new LoadReservationsAsyncCommand(reservationListingService, this).Execute(null);
        
        SortComboBoxList = new ObservableCollection<string>(FilterComboBoxList());

        OpenModal = new OpenModalCommand(navigator, viewModelFactory, () => ViewType.AddCrud);
    }
    
    public bool IsTemporaryViewModelOpened => _messengerCurrentViewStorage.IsTemporaryViewModelOpened;
    public override string ChoosenFilterField { get; set; }
    public override ICollectionView CollectionView { get; set; }
    public ICommand OpenModal { get; }
    
    private Dictionary<string, Func<ReservationDto, string>> FilterByColumn { get; } = new()
    {
        { "Fullname", a=> a.GuestFullName },
        { "Status", a=> a.ReservationStatus }
    };

    public ObservableCollection<ReservationDto> Reservations
    {
        get => _reservations;
        set
        {
            _reservations = value;
            OnPropertyChanged(nameof(Reservations));
            CollectionView = CollectionViewSource.GetDefaultView(Reservations);
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

    // private void GetAllReservations()
    // {
    //     var reservationDtos = _reservationListingService.GetAllReservations();
    //     Reservations = new ObservableCollection<ReservationDto>(reservationDtos);
    // }
    
    protected override List<string> FilterComboBoxList()
    {
        return new List<string>()
        {
            "Fullname",
            "Status"
        };
    }
    
    protected override bool Filter(object obj)
    {
        if (obj is not ReservationDto reservationDto) return false;
        
        FilterByColumn.TryGetValue(ChoosenFilterField, out var propertyAccessor);
        var propertyValue = propertyAccessor(reservationDto);
        return propertyValue?.Contains(FilterField, StringComparison.InvariantCultureIgnoreCase) ?? false;
    }
    
    public override string FilterField
    {
        get { return _reservationsFilterField; }
        set
        {
            _reservationsFilterField = value;
            OnPropertyChanged(nameof(FilterField));
            CollectionView.Filter = Filter;
            CollectionView.Refresh();
        }
    }
}