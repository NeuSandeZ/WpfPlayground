using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using Hotel.Application.DTOS.GuestsListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.Commands;
using Hotel.Factories;
using Hotel.Services.Interfaces;
using Hotel.Stores;

namespace Hotel.MVVM.ViewModels;

public class GuestViewModel : SortingAndFilteringViewModel<GuestDto>
{
    private readonly IGuestsListingService _guestsListingService;
    private readonly MessengerCurrentViewStorage _messengerCurrentViewStorage;
    private readonly INavigator _navigator;
    private readonly IViewModelFactory _viewModelFactory;

    private GuestDto _selectedGuest;

    public GuestViewModel(INavigator navigator, IViewModelFactory viewModelFactory,
        IGuestsListingService guestsListingService, MessengerCurrentViewStorage messengerCurrentViewStorage)
    {
        _navigator = navigator;
        _viewModelFactory = viewModelFactory;
        _guestsListingService = guestsListingService;
        _messengerCurrentViewStorage = messengerCurrentViewStorage;

        GetAllGuests();

        FilterComboBoxList = new ObservableCollection<string>(LoadFilterComboBoxList());
        SortComboBoxList = new ObservableCollection<string>(LoadSortComboBoxList());

        OpenModal = new OpenModalCommand(navigator, viewModelFactory, () => ViewType.AddGuest);
        EditCommand = new EditGuestCommand(navigator, _guestsListingService, this);
        Refresh = new ActionBaseCommand(() => GetAllGuests());
    }


    public GuestDto SelectedGuest
    {
        get => _selectedGuest;
        set
        {
            _selectedGuest = value;
            if (_messengerCurrentViewStorage.IsTemporaryViewModelOpened)
                WeakReferenceMessenger.Default.Send(SelectedGuest);
            OnPropertyChanged();
        }
    }

    public ICommand OpenModal { get; }
    public ICommand EditCommand { get; }
    public ICommand Refresh { get; }

    protected override Dictionary<string, Func<GuestDto, string>> FilterByColumn { get; } = new()
    {
        { "First name", a => a.FirstName },
        { "Last name", a => a.LastName },
        { "Email", a => a.Email },
        { "Phone number", a => a.PhoneNumber },
        { "City", a => a.City },
        { "Street", a => a.Street },
        { "PostalCode", a => a.PostalCode }
    };

    ~GuestViewModel()
    {
        DisposeFilter();
    }

    private async Task GetAllGuests()
    {
        var guestDtos = await _guestsListingService.GetAllGuests();
        Items = new ObservableCollection<GuestDto>(guestDtos);
    }

    public void SendGuestDto()
    {
        WeakReferenceMessenger.Default.Send<string>(SelectedGuest.Email);
    }

    protected override List<string> LoadFilterComboBoxList()
    {
        return new List<string>
        {
            "First name",
            "Last name",
            "Email",
            "Phone number",
            "City",
            "Street",
            "PostalCode"
        };
    }

    protected override void Sort()
    {
        CollectionView.SortDescriptions.Clear();

        var columnToSort = ChoosenSortField switch
        {
            "First name" => nameof(GuestDto.FirstName),
            "Last name" => nameof(GuestDto.LastName),
            "Email" => nameof(GuestDto.Email),
            "Phone number" => nameof(GuestDto.PhoneNumber),
            "City" => nameof(GuestDto.City),
            "Street" => nameof(GuestDto.Street),
            "PostalCode" => nameof(GuestDto.PostalCode),
            _ => null
        };
        SortByAscOrDesc(columnToSort);
    }
}