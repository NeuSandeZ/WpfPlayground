using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

public sealed class ReservationsListingViewModel : SortingAndFilteringViewModel<ReservationDto>
{
    private readonly MessengerCurrentViewStorage _messengerCurrentViewStorage;
    private readonly INavigator _navigator;
    private readonly IReservationListingService _reservationListingService;
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
        //TODO Finally managed to load it asynchronously but sending query to DB everytime i regrab this view is bad idea
        LoadData(reservationListingService);

        FilterComboBoxList = new ObservableCollection<string>(LoadFilterComboBoxList());
        SortComboBoxList = new ObservableCollection<string>(LoadSortComboBoxList());

        OpenModal = new OpenModalCommand(navigator, viewModelFactory, () => ViewType.AddCrud);
        Refresh = new ActionBaseCommand(() => LoadData(reservationListingService));
    }

    public bool IsTemporaryViewModelOpened => _messengerCurrentViewStorage.IsTemporaryViewModelOpened;

    public ICommand Refresh { get; }
    public ICommand OpenModal { get; }


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

    protected override Dictionary<string, Func<ReservationDto, string>> FilterByColumn { get; } = new()
    {
        { "Fullname", a => a.GuestFullName },
        { "Status", a => a.ReservationStatus }
    };

    ~ReservationsListingViewModel()
    {
        DisposeFilter();
    }

    public void LoadData(IReservationListingService reservationListingService)
    {
        new LoadReservationsAsyncCommand(reservationListingService, this).Execute(null);
    }

    //FILTERING
    protected override List<string> LoadFilterComboBoxList()
    {
        return new List<string>
        {
            "Fullname",
            "Status"
        };
    }

    //SORTING

    protected override void Sort()
    {
        CollectionView.SortDescriptions.Clear();

        var columnToSort = ChoosenSortField switch
        {
            //TODO ADD MORE COLUMNS 
            "Check in date" => nameof(ReservationDto.CheckInDate),
            "Check out date" => nameof(ReservationDto.CheckOutDate),
            "Fullname" => nameof(ReservationDto.GuestFullName),
            _ => null
        };
        SortByAscOrDesc(columnToSort);
    }
}