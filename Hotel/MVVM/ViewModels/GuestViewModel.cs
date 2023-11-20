using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Hotel.Application.DTOS.GuestsListingDto;
using Hotel.Application.DTOS.ReservationListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.Commands;
using Hotel.Factories;
using Hotel.Services.Interfaces;

namespace Hotel.MVVM.ViewModels;

public class GuestViewModel : ViewModelBase
{
    private readonly INavigator _navigator;
    private readonly IGuestsListingService _guestsListingService;

    private ObservableCollection<GuestDto> _guestDtos;
    public ObservableCollection<GuestDto> GuestDtos
    {
        get
        {
            return _guestDtos;
        }
        set
        {
            _guestDtos = value;
            OnPropertyChanged(nameof(GuestDtos));
        }
    }
    public GuestViewModel(INavigator navigator, IViewModelFactory viewModelFactory, IGuestsListingService guestsListingService)
    {
        _navigator = navigator;
        _guestsListingService = guestsListingService;

        GetAllGuests();

        OpenModal = new OpenModalCommand(navigator, viewModelFactory, () => ViewType.TextXD);
    }

    public ICommand OpenModal { get; }
    
    private async Task GetAllGuests()
    {
        var guestDtos = await _guestsListingService.GetAllGuests();
        GuestDtos = new ObservableCollection<GuestDto>(guestDtos);
    }
}