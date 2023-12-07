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
        AddPromotion = new AddPromotionCommand(this,_roomListingService);

        GetAllReservations();
    }

    public ICommand AddPromotion { get; }
    public ICommand OpenModal { get; }


    public ObservableCollection<RoomsListingDto> Rooms
    {
        get => _rooms;
        set
        {
            _rooms = value;
            OnPropertyChanged();
        }
    }

    private RoomsListingDto _selectedRoom;

    public RoomsListingDto SelectedRoom
    {
        get { return _selectedRoom; }
        set
        {
            _selectedRoom = value;
            OnPropertyChanged(nameof(SelectedRoom));
        }
    }

    private string _discountAmount;

    public string DiscountAmount
    {
        get { return _discountAmount; }
        set
        {
            _discountAmount = value;
            OnPropertyChanged(nameof(DiscountAmount));
        }
    }
    
    private async Task GetAllReservations()
    {
        var roomsDto = await _roomListingService.GetAllRooms();
        Rooms = new ObservableCollection<RoomsListingDto>(roomsDto);
    }
}