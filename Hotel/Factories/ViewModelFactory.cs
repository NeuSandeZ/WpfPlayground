using System;
using CommunityToolkit.Mvvm.Messaging;
using Hotel.MVVM.ViewModels;
using Hotel.MVVM.ViewModels.Modals;
using Hotel.Services;
using Hotel.Services.Interfaces;

namespace Hotel.Factories;

public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : ViewModelBase;
public class ViewModelFactory : IViewModelFactory
{
    private readonly INavigator _navigator;
    //VMS
    private readonly CreateViewModel<ReservationsListingViewModel> _createReservationViewModel;
    private readonly CreateViewModel<GuestViewModel> _createGuestViewModel;
    private readonly CreateViewModel<RoomsViewModel> _createRoomsViewModel;
    private readonly CreateViewModel<PaymentViewModel> _createPaymentViewModel;
    
    //MODALS
    private readonly CreateViewModel<AddReservationViewModel> _createAddModalView;
    private readonly CreateViewModel<AddGuestViewModel> _creatTestXDViewModel;

    public ViewModelFactory(CreateViewModel<GuestViewModel> createTestViewModel,
        CreateViewModel<ReservationsListingViewModel> createReservationViewModel,
        CreateViewModel<AddReservationViewModel> createAddModalView,
        CreateViewModel<AddGuestViewModel> creatTestXdViewModel,
        CreateViewModel<RoomsViewModel> createRoomsViewModel,
        CreateViewModel<PaymentViewModel> createPaymentViewModel, 
        INavigator navigator)
    {
        WeakReferenceMessenger.Default.Register<string>(this, Open);
        
        _createGuestViewModel = createTestViewModel;
        _createReservationViewModel = createReservationViewModel;
        _createAddModalView = createAddModalView;
        _creatTestXDViewModel = creatTestXdViewModel;
        _createRoomsViewModel = createRoomsViewModel;
        _createPaymentViewModel = createPaymentViewModel;
        _navigator = navigator;
    }

    private void Open(object recipient, string message)
    {
        if (message == "Open")
        {
           _navigator.CurrentViewModel = _createReservationViewModel();
        }

        if (message == "Close")
        {
            _navigator.CurrentViewModel = _createPaymentViewModel();
        }
    }
    public ViewModelBase CreateViewModel(ViewType viewType)
    {
        return viewType switch
        {
            ViewType.Reservation => _createReservationViewModel(),
            ViewType.Guest => _createGuestViewModel(),
            ViewType.Rooms => _createRoomsViewModel(),
            ViewType.Payments => _createPaymentViewModel(),
            ViewType.TextXD => _creatTestXDViewModel(),
            ViewType.AddCrud => _createAddModalView(),
            _ => throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType")
        };
    }
}