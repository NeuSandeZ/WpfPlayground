using System;
using Hotel.MVVM.ViewModels;
using Hotel.Services.Interfaces;

namespace Hotel.Factories;

public class ViewModelFactory : IViewModelFactory
{
    //VMS
    private readonly CreateViewModel<ReservationsListingViewModel> _createReservationViewModel;
    private readonly CreateViewModel<GuestViewModel> _createGuestViewModel;
    private readonly CreateViewModel<RoomsViewModel> _createRoomsViewModel;
    
    //MODALS
    private readonly CreateViewModel<AddReservationViewModel> _createAddModalView;
    private readonly CreateViewModel<AddGuestViewModel> _creatTestXDViewModel;

    public ViewModelFactory(CreateViewModel<GuestViewModel> createTestViewModel,
        CreateViewModel<ReservationsListingViewModel> createReservationViewModel,
        CreateViewModel<AddReservationViewModel> createAddModalView,
        CreateViewModel<AddGuestViewModel> creatTestXdViewModel,
        CreateViewModel<RoomsViewModel> createRoomsViewModel)
    {
        _createGuestViewModel = createTestViewModel;
        _createReservationViewModel = createReservationViewModel;
        _createAddModalView = createAddModalView;
        _creatTestXDViewModel = creatTestXdViewModel;
        _createRoomsViewModel = createRoomsViewModel;
    }

    public ViewModelBase CreateViewModel(ViewType viewType)
    {
        return viewType switch
        {
            ViewType.Reservation => _createReservationViewModel(),
            ViewType.Guest => _createGuestViewModel(),
            ViewType.Rooms => _createRoomsViewModel(),
            ViewType.TextXD => _creatTestXDViewModel(),
            ViewType.AddCrud => _createAddModalView(),
            
            _ => throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType")
        };
    }
}