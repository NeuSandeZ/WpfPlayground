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
 using Hotel.Services.Interfaces;
 using ReservationDto = Hotel.Application.ReservationListingDto.ReservationDto;
 
 namespace Hotel.MVVM.ViewModels;
 
 public class ReservationsListingViewModel : ViewModelBase
 {
     private readonly INavigator _navigator;
     private readonly IReservationListingService _reservationListingService;
     
     public ReservationsListingViewModel(INavigator navigator, IViewModelFactory viewModelFactory,
         IReservationListingService reservationListingService)
     {
         _navigator = navigator;
         _reservationListingService = reservationListingService;
         
         GetAllReservations();
 
         OpenModal = new OpenModalCommand(navigator, viewModelFactory, () => ViewType.AddCrud);
     }
 
     public ICommand OpenModal { get; }
     
     private ObservableCollection<ReservationDto> _reservations;
     public ObservableCollection<ReservationDto> Reservations
     {
         get
         {
             return _reservations;
         }
         set
         {
             _reservations = value;
             OnPropertyChanged(nameof(Reservations));
         }
     }
 
     private async Task GetAllReservations()
     {
         var reservationDtos = await _reservationListingService.GetAllReservations();
         Reservations = new ObservableCollection<ReservationDto>(reservationDtos);
     }
 }