using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Hotel.Application.DTOS.RoomsListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.Commands;
using Hotel.Factories;
using Hotel.Services.Interfaces;

namespace Hotel.MVVM.ViewModels;

public class RoomsViewModel : ViewModelBase
{
    private readonly IRoomListingService _roomListingService;
    private ObservableCollection<RoomsListingDto> _rooms;

    public RoomsViewModel(IRoomListingService roomListingService, INavigator navigator, IViewModelFactory viewModelFactory)
    {
        _roomListingService = roomListingService;
        OpenModal = new OpenModalCommand(navigator,viewModelFactory, () => ViewType.AddRoom);

        GetAllReservations();
    }

    public ObservableCollection<RoomsListingDto> Rooms
    {
        get => _rooms;
        set
        {
            _rooms = value;
            OnPropertyChanged();
        }
    }

    public ICommand OpenModal { get; }

    private async Task GetAllReservations()
    {
        var roomsDto = await _roomListingService.GetAllRooms();
        Rooms = new ObservableCollection<RoomsListingDto>(roomsDto);
    }
}