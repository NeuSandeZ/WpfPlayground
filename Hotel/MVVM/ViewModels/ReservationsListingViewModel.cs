using System;
 using System.Collections.Generic;
 using System.Collections.ObjectModel;
 using System.Linq;
 using System.Threading.Tasks;
 using System.Windows.Input;
 using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Hotel.Application.DTOS.ReservationListingDto;
using Hotel.Application.Services.Interfaces;
 using Hotel.Commands;
 using Hotel.Domain.Entities;
 using Hotel.Domain.IRepositories;
 using Hotel.Factories;
 using Hotel.Infrastructure;
 using Hotel.Services.Interfaces;
 
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
         
         //TODO Sending query to database everytime i regrab that view is a bad idea, prolly have to figure out how to load it asynchronously and cache it
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

     private ReservationDto _selectedReservation;
     public ReservationDto SelectedReservation
     {
         get { return _selectedReservation; }
         set
         {
             _selectedReservation = value;
             
             //TODO sending messenger, Do i have to register it in DI? Probably yes
             var reservationDto = WeakReferenceMessenger.Default.Send(SelectedReservation);

             OnPropertyChanged(nameof(SelectedReservation));
         }
     }
 
     private async Task GetAllReservations()
     {
         var reservationDtos = await _reservationListingService.GetAllReservations();
         Reservations = new ObservableCollection<ReservationDto>(reservationDtos);
     }
 }