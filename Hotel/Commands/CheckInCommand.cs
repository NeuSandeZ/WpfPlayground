using System.ComponentModel;
using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using Hotel.Application.DTOS.CheckInsOutsDto;
using Hotel.Application.DTOS.ReservationListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.MVVM.ViewModels;
using Hotel.MVVM.ViewModels.Modals;
using Hotel.Stores;

namespace Hotel.Commands;

public class CheckInCommand : BaseCommand
{
    private readonly CheckInsOutsViewModel _checkInsOutsViewModel;
    private readonly ICheckInOutService _checkInOutService;
    
    public CheckInCommand(CheckInsOutsViewModel checkInsOutsViewModel, ICheckInOutService checkInOutService)
    {
        _checkInsOutsViewModel = checkInsOutsViewModel;
        _checkInOutService = checkInOutService;
        _checkInsOutsViewModel.PropertyChanged += OnModelPropertyChanged;
    }


    public override void Execute(object? parameter)
    {
        var checkInDto = new CheckInDto()
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