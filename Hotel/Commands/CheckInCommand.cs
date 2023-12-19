using System.ComponentModel;
using Hotel.Application.DTOS.CheckInsOutsDto;
using Hotel.Application.Services.Interfaces;
using Hotel.MVVM.ViewModels;

namespace Hotel.Commands;

public class CheckInCommand : BaseCommand
{
    private readonly ICheckInOutService _checkInOutService;
    private readonly CheckInsOutsViewModel _checkInsOutsViewModel;

    public CheckInCommand(CheckInsOutsViewModel checkInsOutsViewModel, ICheckInOutService checkInOutService)
    {
        _checkInsOutsViewModel = checkInsOutsViewModel;
        _checkInOutService = checkInOutService;
        _checkInsOutsViewModel.PropertyChanged += OnModelPropertyChanged;
    }


    public override void Execute(object? parameter)
    {
        var checkInDto = new CheckInDto
        {
            RoomId = _checkInsOutsViewModel.SelectedReservation.RoomId,
            GuestId = _checkInsOutsViewModel.SelectedReservation.GuestId,
            ReservationId = _checkInsOutsViewModel.SelectedReservation.ReservationId
        };
        _checkInOutService.CreateCheckIn(checkInDto);
        _checkInsOutsViewModel.PropertyChanged -= OnModelPropertyChanged;
    }

    public override bool CanExecute(object? parameter)
    {
        return _checkInsOutsViewModel.SelectedReservation != null && base.CanExecute(parameter);
    }

    private void OnModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(CheckInsOutsViewModel.SelectedReservation)) OnCanExecutedChanged();
    }
}