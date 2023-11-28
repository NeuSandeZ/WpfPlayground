using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using Hotel.Application.DTOS.GuestsListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.Commands;
using Hotel.Factories;
using Hotel.MVVM.ViewModels.Modals;
using Hotel.Services.Interfaces;

namespace Hotel.MVVM.ViewModels;

public class GuestViewModel : ViewModelBase
{
    private readonly AddGuestViewModel _addGuestViewModel;
    private readonly IGuestsListingService _guestsListingService;
    private readonly INavigator _navigator;
    private readonly IViewModelFactory _viewModelFactory;

    private ObservableCollection<GuestDto> _guestDtos;

    private GuestDto _selectedGuest;

    public GuestViewModel(INavigator navigator, IViewModelFactory viewModelFactory,
        IGuestsListingService guestsListingService, AddGuestViewModel addGuestViewModel)
    {
        _navigator = navigator;
        _viewModelFactory = viewModelFactory;
        _guestsListingService = guestsListingService;
        _addGuestViewModel = addGuestViewModel;

        GetAllGuests();
        OpenModal = new OpenModalCommand(navigator, viewModelFactory, () => ViewType.AddGuest);
        EditCommand = new EditGuestCommand(navigator, viewModelFactory, _guestsListingService, this);
    }

    public ObservableCollection<GuestDto> GuestDtos
    {
        get => _guestDtos;
        set
        {
            _guestDtos = value;
            OnPropertyChanged();
        }
    }

    public GuestDto SelectedGuest
    {
        get => _selectedGuest;
        set
        {
            _selectedGuest = value;
            OnPropertyChanged();
        }
    }

    public ICommand OpenModal { get; }
    public ICommand EditCommand { get; }

    public async Task GetAllGuests()
    {
        var guestDtos = await _guestsListingService.GetAllGuests();
        GuestDtos = new ObservableCollection<GuestDto>(guestDtos);
    }

    public void SendGuestDto()
    {
        WeakReferenceMessenger.Default.Send<string>(SelectedGuest.Email);
    }
}