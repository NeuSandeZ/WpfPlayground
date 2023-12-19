using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Hotel.Application.DTOS.RoomsListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.Commands;
using Hotel.Factories;
using Hotel.Services.Interfaces;

namespace Hotel.MVVM.ViewModels;

public class RoomsViewModel : SortingAndFilteringViewModel<RoomsListingDto>
{
    private readonly IRoomListingService _roomListingService;

    private string _discountAmount;


    private RoomsListingDto _selectedRoom;
    // private ObservableCollection<RoomsListingDto> _rooms;

    public RoomsViewModel(IRoomListingService roomListingService, INavigator navigator,
        IViewModelFactory viewModelFactory)
    {
        _roomListingService = roomListingService;

        GetAllRooms();

        FilterComboBoxList = new ObservableCollection<string>(LoadFilterComboBoxList());
        SortComboBoxList = new ObservableCollection<string>(LoadSortComboBoxList());

        OpenModal = new OpenModalCommand(navigator, viewModelFactory, () => ViewType.AddRoom);
        AddPromotion = new AddPromotionCommand(this, _roomListingService);
        Refresh = new ActionBaseCommand(() => GetAllRooms());
    }

    public ICommand AddPromotion { get; }
    public ICommand OpenModal { get; }
    public ICommand Refresh { get; }

    public RoomsListingDto SelectedRoom
    {
        get => _selectedRoom;
        set
        {
            _selectedRoom = value;
            OnPropertyChanged();
        }
    }

    public string DiscountAmount
    {
        get => _discountAmount;
        set
        {
            _discountAmount = value;
            OnPropertyChanged();
        }
    }

    protected override Dictionary<string, Func<RoomsListingDto, string>> FilterByColumn { get; } = new()
    {
        { "Room number", a => a.RoomNumber },
        { "Floor number", a => a.FloorNumber },
        { "Price per night", a => a.PricePerNight },
        { "Room status", a => a.RoomStatus },
        { "Room type", a => a.PricePerNight },
        { "Room promotion ", a => a.RoomPromotion }
    };

    ~RoomsViewModel()
    {
        CollectionView.Filter -= Filter;
    }

    private async Task GetAllRooms()
    {
        var roomsDto = await _roomListingService.GetAllRooms();
        Items = new ObservableCollection<RoomsListingDto>(roomsDto);
    }

    protected override List<string> LoadFilterComboBoxList()
    {
        return new List<string>
        {
            "Room number",
            "Floor number",
            "Room type",
            "Room status",
            "Price per night"
        };
    }

    protected override void Sort()
    {
        CollectionView.SortDescriptions.Clear();

        var columnToSort = ChoosenSortField switch
        {
            "Room number" => nameof(RoomsListingDto.RoomNumber),
            "Floor number" => nameof(RoomsListingDto.FloorNumber),
            "Room type" => nameof(RoomsListingDto.RoomType),
            "Room status" => nameof(RoomsListingDto.RoomStatus),
            "Price per night" => nameof(RoomsListingDto.PricePerNight),
            _ => null
        };
        SortByAscOrDesc(columnToSort);
    }
}