using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Hotel.Application.DTOS.CheckInsOutsDto;
using Hotel.Application.Services.Interfaces;
using Hotel.Commands;
using Hotel.Commands.AsyncCommands;

namespace Hotel.MVVM.ViewModels;

public class CheckInsOutsViewModel : SortingAndFilteringViewModel<CheckInListingDto>
{
    private readonly ICheckInOutService _checkInOutService;

    private IQueryable<ReservationComboBoxDto> _roomsGuestsReservations;

    private CheckInListingDto _selectedCheckIn;

    private ReservationComboBoxDto _selectedReservation;

    private string _todaysCheckIns;

    private string _todaysCheckOuts;

    public CheckInsOutsViewModel(ICheckInOutService checkInOutService)
    {
        _checkInOutService = checkInOutService;
        CheckInCommand = new CheckInCommand(this, _checkInOutService);
        CheckOutCommand = new CheckOutCommand(_checkInOutService, this);

        LoadData();
        FilterComboBoxList = new ObservableCollection<string>(LoadFilterComboBoxList());
        SortComboBoxList = new ObservableCollection<string>(LoadSortComboBoxList());

        Refresh = new ActionBaseCommand(() => LoadData());
    }

    public ICommand CheckInCommand { get; }
    public ICommand CheckOutCommand { get; }
    public ICommand LoadCheckInsAsyncCommand { get; }
    public ICommand Refresh { get; }

    public IQueryable<ReservationComboBoxDto> RoomsGuestsReservations
    {
        get => _roomsGuestsReservations;
        set
        {
            _roomsGuestsReservations = value;
            OnPropertyChanged();
        }
    }

    public string TodaysCheckIns
    {
        get => _todaysCheckIns;
        set
        {
            _todaysCheckIns = value;
            OnPropertyChanged();
        }
    }

    public string TodaysCheckOuts
    {
        get => _todaysCheckOuts;
        set
        {
            _todaysCheckOuts = value;
            OnPropertyChanged();
        }
    }

    public ReservationComboBoxDto SelectedReservation
    {
        get => _selectedReservation;
        set
        {
            _selectedReservation = value;
            OnPropertyChanged();
        }
    }

    public CheckInListingDto SelectedCheckIn
    {
        get => _selectedCheckIn;
        set
        {
            _selectedCheckIn = value;
            OnPropertyChanged();
        }
    }

    protected override Dictionary<string, Func<CheckInListingDto, string>> FilterByColumn { get; } = new()
    {
        { "Floor and room number", a => a.FloorAndRoomNumber },
        { "Reservation number", a => a.ReservationNumber },
        { "Fullname", a => a.FullGuestName },
        { "Check in date", a => a.CheckInDate.ToString() },
        { "Check out date", a => a.CheckOutDate.ToString() }
    };

    private void LoadData()
    {
        new LoadCheckInsAsyncCommand(_checkInOutService, this).Execute(null);
    }

    protected override List<string> LoadFilterComboBoxList()
    {
        return new List<string>
        {
            "Floor and room number",
            "Reservation number",
            "Fullname",
            "Check in date",
            "Check out date"
        };
    }

    protected override void Sort()
    {
        CollectionView.SortDescriptions.Clear();

        var columnToSort = ChoosenSortField switch
        {
            "Floor and room number" => nameof(CheckInListingDto.FloorAndRoomNumber),
            "Reservation number" => nameof(CheckInListingDto.ReservationNumber),
            "Fullname" => nameof(CheckInListingDto.FullGuestName),
            "Check in date" => nameof(CheckInListingDto.CheckInDate),
            "Check out date" => nameof(CheckInListingDto.CheckOutDate),
            _ => null
        };
        SortByAscOrDesc(columnToSort);
    }
}