using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Hotel.Application.DTOS.CheckInsOutsDto;
using Hotel.Application.Services.Interfaces;
using Hotel.MVVM.ViewModels;

namespace Hotel.Commands.AsyncCommands;

public class LoadCheckInsAsyncCommand : AsyncCommandBase
{
    private readonly ICheckInOutService _checkInOutService;
    private readonly CheckInsOutsViewModel _insOutsViewModel;

    public LoadCheckInsAsyncCommand(ICheckInOutService checkInOutService, CheckInsOutsViewModel insOutsViewModel)
    {
        _checkInOutService = checkInOutService;
        _insOutsViewModel = insOutsViewModel;
    }

    protected override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            //TODO Turns out i have to create instance of dbcontext foreach call to database in order to use Task.WhenAll();
            await GetAllCheckIns();
            await GetAllRoomsWithResarvationsAndGuests();
            await GetCheckIns();
            await GetCheckOuts();
        }
        catch (Exception)
        {
            MessageBox.Show("Failed to load!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async Task GetAllRoomsWithResarvationsAndGuests()
    {
        var roomsWithReservationsAndGuests = await _checkInOutService.GetAllReservationNumbers();
        _insOutsViewModel.RoomsGuestsReservations = roomsWithReservationsAndGuests.AsQueryable();
    }

    private async Task GetAllCheckIns()
    {
        var checkInListingDtos = await _checkInOutService.GetAllCheckIns();
        _insOutsViewModel.Items = new ObservableCollection<CheckInListingDto>(checkInListingDtos);
    }

    private async Task GetCheckIns()
    {
        var checkIns = await _checkInOutService.GetTodaysCheckIns();
        _insOutsViewModel.TodaysCheckIns = checkIns.ToString();
    }

    private async Task GetCheckOuts()
    {
        var checkOuts = await _checkInOutService.GetTodaysCheckOuts();
        _insOutsViewModel.TodaysCheckOuts = checkOuts.ToString();
    }
}