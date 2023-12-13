using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Hotel.Application.DTOS.CheckInsOutsDto;
using Hotel.Application.Services;
using Hotel.Application.Services;
using Hotel.Application.Services.Interfaces;
using Hotel.Commands;
using Hotel.Commands.AsyncCommands;

namespace Hotel.MVVM.ViewModels;

public class CheckInsOutsViewModel : ViewModelBase
{
    private readonly ICheckInOutService _checkInOutService;
    
    public CheckInsOutsViewModel(ICheckInOutService checkInOutService)
    {
        _checkInOutService = checkInOutService;
        CheckInCommand = new CheckInCommand(this,_checkInOutService);
        CheckOutCommand = new CheckOutCommand(_checkInOutService,this);
        
        new LoadCheckInsAsyncCommand(_checkInOutService, this).Execute(null);
    }
    public ICommand CheckInCommand { get; }
    public ICommand CheckOutCommand { get; }
    public ICommand LoadCheckInsAsyncCommand { get; }

    private IQueryable<ReservationComboBoxDto> _roomsGuestsReservations;

    public IQueryable<ReservationComboBoxDto> RoomsGuestsReservations
    {
        get { return _roomsGuestsReservations; }
        set
        {
            _roomsGuestsReservations = value;
            OnPropertyChanged(nameof(RoomsGuestsReservations));
        }
    }

    private string _todaysCheckIns;

    public string TodaysCheckIns
    {
        get { return _todaysCheckIns; }
        set
        {
            _todaysCheckIns = value;
            OnPropertyChanged(nameof(TodaysCheckIns));
        }
    }

    private string _todaysCheckOuts;

    public string TodaysCheckOuts
    {
        get { return _todaysCheckOuts; }
        set
        {
            _todaysCheckOuts = value;
            OnPropertyChanged(nameof(TodaysCheckOuts));
        }
    }
    
    private ReservationComboBoxDto _selectedReservation;

    public ReservationComboBoxDto SelectedReservation
    {
        get { return _selectedReservation; }
        set
        {
            _selectedReservation = value;
            OnPropertyChanged(nameof(SelectedReservation));
        }
    }

    private ObservableCollection<CheckInListingDto> _allCheckIns;

    public ObservableCollection<CheckInListingDto> AllCheckIns
    {
        get { return _allCheckIns; }
        set
        {
            _allCheckIns = value;
            OnPropertyChanged(nameof(AllCheckIns));
        }
    }

    private CheckInListingDto _selectedCheckIn;

    public CheckInListingDto SelectedCheckIn
    {
        get { return _selectedCheckIn; }
        set
        {
            _selectedCheckIn = value;
            OnPropertyChanged(nameof(SelectedCheckIn));
        }
    }
}