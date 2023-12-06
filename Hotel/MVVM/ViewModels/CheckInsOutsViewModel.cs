using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Hotel.Application.DTOS.CheckInsOutsDto;
using Hotel.Application.Services;
using Hotel.Application.Services;
using Hotel.Application.Services.Interfaces;
using Hotel.Commands;

namespace Hotel.MVVM.ViewModels;

public class CheckInsOutsViewModel : ViewModelBase
{
    private readonly ICheckInOutService _checkInOutService;
    
    public CheckInsOutsViewModel(ICheckInOutService checkInOutService)
    {
        _checkInOutService = checkInOutService;
        CheckInCommand = new CheckInCommand(this,_checkInOutService);
        CheckOutCommand = new CheckOutCommand(_checkInOutService,this);

        GetCheckIns();
        GetCheckOuts();
        GetAllCheckIns();
        
        GetAllRoomsWithResarvationsAndGuests();
    }
    public ICommand CheckInCommand { get; }
    public ICommand CheckOutCommand { get; }

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

    private void GetAllRoomsWithResarvationsAndGuests()
    {
        RoomsGuestsReservations = _checkInOutService.GetAllReservationNumbers().AsQueryable();
    }

    private void GetAllCheckIns()
    {
        var checkInListingDtos = _checkInOutService.GetAllCheckIns();
        AllCheckIns = new ObservableCollection<CheckInListingDto>(checkInListingDtos) ;
    }

    private void GetCheckIns()
    {
        TodaysCheckIns = _checkInOutService.GetTodaysCheckIns().ToString();
    }
    
    private void GetCheckOuts()
    {
        TodaysCheckOuts = _checkInOutService.GetTodaysCheckOuts().ToString();
    }
}