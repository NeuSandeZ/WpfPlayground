using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Navigation;
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
        LoadData(reservationListingService);
        
        FilterComboBoxList = new ObservableCollection<string>(LoadFilterComboBoxList());
        SortComboBoxList = new ObservableCollection<string>(LoadSortComboBoxList());

        OpenModal = new OpenModalCommand(navigator, viewModelFactory, () => ViewType.AddCrud);
        Refresh = new ActionBaseCommand(()=>LoadData(reservationListingService));
    }

    ~ReservationsListingViewModel()
    {
        CollectionView.Filter -= Filter;
    }

    public void LoadData(IReservationListingService reservationListingService)
    {
        new LoadReservationsAsyncCommand(reservationListingService, this).Execute(null);
    }

    public bool IsTemporaryViewModelOpened => _messengerCurrentViewStorage.IsTemporaryViewModelOpened;
   
    public ICommand Refresh { get; }
    public ICommand OpenModal { get; }
    
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
    
    //FILTERING
    public override string ChoosenFilterField { get; set; }
    public override ICollectionView CollectionView { get; set; }
    
    protected override List<string> LoadFilterComboBoxList()
    {
        return new List<string>()
        {
            "Fullname",
            "Status"
        };
    }
    
    private Dictionary<string, Func<ReservationDto, string>> FilterByColumn { get; } = new()
    {
        { "Fullname", a=> a.GuestFullName },
        { "Status", a=> a.ReservationStatus }
    };
    
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
            CollectionView.Filter += Filter;
            CollectionView.Refresh();
        }
    }
    
    //SORTING
    protected override List<string> LoadSortComboBoxList()
    {
        return new List<string>()
        {
            "Check in date", "Check out date", "Fullname"
        };
    }
    
    public override string? ChoosenSortField
    {
        get => _choosenSortField;
        set
        {
            _choosenSortField = value;
            OnPropertyChanged(ChoosenSortField);
            // SORTING RIGHT AFTER PICKING A COLUMN
            // Sort();
        }
    }

    public override ComboBoxItem ChoosenDirectionSort
    {
        get => _choosenDirectionSort;
        set
        {
            _choosenDirectionSort = value;
            OnPropertyChanged(nameof(ChoosenDirectionSort));
            // SORT ON SELECTION
            // if (ChoosenSortField is not null)
            // {
            //     Sort();
            // }
        }
    }
    
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

        if (columnToSort is null)
        { 
            MessageBox.Show("Choose the column you want to sort on!");
            return;
        }

        CollectionView.SortDescriptions.Add(new SortDescription(columnToSort, (string)ChoosenDirectionSort?.Content == "Ascending" 
                                                                              || (string)ChoosenDirectionSort?.Content == null  ? ListSortDirection.Ascending : ListSortDirection.Descending));
    }
}