using System;
using Hotel.MVVM.ViewModels;
using Hotel.MVVM.ViewModels.Modals;
using Hotel.Services.Interfaces;

namespace Hotel.Factories;

public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : ViewModelBase;

public class ViewModelFactory : IViewModelFactory
{
    //MODALS
    private readonly CreateViewModel<AddReservationViewModel> _createAddModalView;
    private readonly CreateViewModel<GuestViewModel> _createGuestViewModel;

    private readonly CreateViewModel<PaymentViewModel> _createPaymentViewModel;

    //VMS
    private readonly CreateViewModel<ReservationsListingViewModel> _createReservationViewModel;
    private readonly CreateViewModel<RoomsViewModel> _createRoomsViewModel;
    private readonly CreateViewModel<AddGuestViewModel> _creatGuestViewModal;

    public ViewModelFactory(CreateViewModel<GuestViewModel> createTestViewModel,
        CreateViewModel<ReservationsListingViewModel> createReservationViewModel,
        CreateViewModel<AddReservationViewModel> createAddModalView,
        CreateViewModel<AddGuestViewModel> creatGuestViewModal,
        CreateViewModel<RoomsViewModel> createRoomsViewModel,
        CreateViewModel<PaymentViewModel> createPaymentViewModel)
    {
        _createGuestViewModel = createTestViewModel;
        _createReservationViewModel = createReservationViewModel;
        _createAddModalView = createAddModalView;
        _creatGuestViewModal = creatGuestViewModal;
        _createRoomsViewModel = createRoomsViewModel;
        _createPaymentViewModel = createPaymentViewModel;
    }

    public ViewModelBase CreateViewModel(ViewType viewType)
    {
        return viewType switch
        {
            ViewType.Reservation => _createReservationViewModel(),
            ViewType.Guest => _createGuestViewModel(),
            ViewType.Rooms => _createRoomsViewModel(),
            ViewType.Payments => _createPaymentViewModel(),
            ViewType.AddGuest => _creatGuestViewModal(),
            ViewType.AddCrud => _createAddModalView(),
            _ => throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType")
        };
    }
}