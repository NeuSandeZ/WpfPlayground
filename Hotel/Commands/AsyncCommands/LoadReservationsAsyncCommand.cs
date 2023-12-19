using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using Hotel.Application.DTOS.ReservationListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.MVVM.ViewModels;

namespace Hotel.Commands.AsyncCommands;

public class LoadReservationsAsyncCommand : AsyncCommandBase
{
    private readonly IReservationListingService _reservationListingService;
    private readonly ReservationsListingViewModel _reservationsListingViewModel;

    public LoadReservationsAsyncCommand(IReservationListingService reservationListingService,
        ReservationsListingViewModel reservationsListingViewModel)
    {
        _reservationListingService = reservationListingService;
        _reservationsListingViewModel = reservationsListingViewModel;
    }

    protected override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            var allReservations = await _reservationListingService.GetAllReservations();
            _reservationsListingViewModel.Items = new ObservableCollection<ReservationDto>(allReservations);
        }
        catch (Exception)
        {
            MessageBox.Show("Failed to load reservations!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}