using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using Hotel.Application.DTOS.GuestsListingDto;
using Hotel.Application.DTOS.ReservationListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.Commands;
using Hotel.Factories;
using Hotel.MVVM.ViewModels.Modals;
using Hotel.Services.Interfaces;
using Hotel.Stores;

namespace Hotel.MVVM.ViewModels;

public class GuestViewModel : ViewModelBase
{
    private readonly IGuestsListingService _guestsListingService;
    private readonly INavigator _navigator;
    private readonly IViewModelFactory _viewModelFactory;
    private readonly MessengerCurrentViewStorage _messengerCurrentViewStorage;

    private ObservableCollection<GuestDto> _guestDtos;
    private GuestDto _selectedGuest;
    
    public GuestViewModel(INavigator navigator, IViewModelFactory viewModelFactory,
        IGuestsListingService guestsListingService, MessengerCurrentViewStorage messengerCurrentViewStorage)
    {
        _navigator = navigator;
        _viewModelFactory = viewModelFactory;
        _guestsListingService = guestsListingService;
        _messengerCurrentViewStorage = messengerCurrentViewStorage;
     
        GetAllGuests();
        
        OpenModal = new OpenModalCommand(navigator, viewModelFactory, () => ViewType.AddGuest);
        EditCommand = new EditGuestCommand(navigator, _guestsListingService, this);
    }

    public ObservableCollection<GuestDto> GuestDtos
    {
        get => _guestDtos;
        set
        {
            _guestDtos = value;
            OnPropertyChanged(nameof(GuestDtos));
        }
    }

    public GuestDto SelectedGuest
    {
        get => _selectedGuest;
        set
        {
            _selectedGuest = value;
            if (_messengerCurrentViewStorage.IsTemporaryViewModelOpened)
            {
                WeakReferenceMessenger.Default.Send(SelectedGuest);
            }
            OnPropertyChanged(nameof(SelectedGuest));
        }
    }
    
    public ICommand OpenModal { get; }
    public ICommand EditCommand { get; }

    private async Task GetAllGuests()
    {
        var guestDtos = await _guestsListingService.GetAllGuests();
        GuestDtos = new ObservableCollection<GuestDto>(guestDtos);
    }

    public void SendGuestDto()
    {
        WeakReferenceMessenger.Default.Send<string>(SelectedGuest.Email);
    }
}