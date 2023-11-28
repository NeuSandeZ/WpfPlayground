using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Hotel.Application.DTOS.RoomsListingDto;
using Hotel.Application.Services.Interfaces;

namespace Hotel.MVVM.ViewModels;

public class RoomsViewModel : ViewModelBase
{
    private readonly IRoomListingService _roomListingService;

    private ObservableCollection<RoomsListingDto> _rooms;

    public RoomsViewModel(IRoomListingService roomListingService)
    {
        _roomListingService = roomListingService;

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

    private async Task GetAllReservations()
    {
        var roomsDto = await _roomListingService.GetAllRooms();
        Rooms = new ObservableCollection<RoomsListingDto>(roomsDto);
    }
}