using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Hotel.Application.Services.Interfaces;
using Hotel.Commands;
using Hotel.Domain.Entities;
using Hotel.Domain.IRepositories;
using Hotel.Factories;
using Hotel.Infrastructure;
using Hotel.MVVM.Models;
using Hotel.Services.Interfaces;

namespace Hotel.MVVM.ViewModels;

public class ReservationsListingViewModel : ViewModelBase
{
    private readonly INavigator _navigator;
    private readonly IReservationListingService _reservationListingService;
  

    public ReservationsListingViewModel(INavigator navigator, IViewModelFactory viewModelFactory,
        IReservationListingService reservationListingService, HotelDbContext dbContext)
    {
        _navigator = navigator;
        _reservationListingService = reservationListingService;

        OpenModal = new OpenModalCommand(navigator, viewModelFactory, () => ViewType.AddCrud);
    }

    public ICommand OpenModal { get; }

    [ObservableProperty]
    private ObservableCollection<Reservation> _reservations;

    // public ObservableCollection<Reservation> Reservations
    // {
    //     get
    //     {
    //         return _reservations = Task.Run(()=>Load()).Result;
    //     }
    //     set
    //     {
    //         _reservations = value;
    //         OnPropertyChanged(nameof(Reservations));
    //     }
    // }

    // private ObservableCollection<Reservation> Load()
    // {
    //     var reservations = GetAllReservations().Result;
    //     return new ObservableCollection<Reservation>(reservations);
    // }
   

    public async Task LoadDataAsync()
    {
        var reservations = await _reservationListingService.GetAll();
        _reservations = new ObservableCollection<Reservation>(reservations);
    }
}