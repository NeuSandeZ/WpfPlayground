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
    private readonly CreateViewModel<AddGuestViewModel> _creatGuestViewModal;
    private readonly CreateViewModel<AddRoomViewModel> _createRoomViewModal;
    private readonly CreateViewModel<AddStaffViewModel> _createAddStaffViewModel;
    
    //VMS
    private readonly CreateViewModel<ReservationsListingViewModel> _createReservationViewModel;
    private readonly CreateViewModel<RoomsViewModel> _createRoomsViewModel;
    private readonly CreateViewModel<PaymentViewModel> _createPaymentViewModel;
    private readonly CreateViewModel<GuestViewModel> _createGuestViewModel;
    private readonly CreateViewModel<CheckInsOutsViewModel> _createCheckInsViewModel;
    private readonly CreateViewModel<StaffViewModel> _createStaffViewModel;
    private readonly CreateViewModel<TasksViewModel> _createTasksViewModel;


    //TODO i think i should find the way to create object with the ViewModel needed instead of creating all at once and choosing from them
    public ViewModelFactory(CreateViewModel<GuestViewModel> createTestViewModel,
        CreateViewModel<ReservationsListingViewModel> createReservationViewModel,
        CreateViewModel<AddReservationViewModel> createAddModalView,
        CreateViewModel<AddGuestViewModel> creatGuestViewModal,
        CreateViewModel<RoomsViewModel> createRoomsViewModel,
        CreateViewModel<PaymentViewModel> createPaymentViewModel,
        CreateViewModel<AddRoomViewModel> createRoomViewModal, 
        CreateViewModel<CheckInsOutsViewModel> createCheckInsViewModel,
        CreateViewModel<StaffViewModel> createStaffViewModel, 
        CreateViewModel<TasksViewModel> createTasksViewModel,
        CreateViewModel<AddStaffViewModel> createAddStaffViewModel)
    {
        _createGuestViewModel = createTestViewModel;
        _createReservationViewModel = createReservationViewModel;
        _createAddModalView = createAddModalView;
        _creatGuestViewModal = creatGuestViewModal;
        _createRoomsViewModel = createRoomsViewModel;
        _createPaymentViewModel = createPaymentViewModel;
        _createRoomViewModal = createRoomViewModal;
        _createCheckInsViewModel = createCheckInsViewModel;
        _createStaffViewModel = createStaffViewModel;
        _createTasksViewModel = createTasksViewModel;
        _createAddStaffViewModel = createAddStaffViewModel;
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
            ViewType.AddStaff => _createAddStaffViewModel(),
            ViewType.CheckInsOuts => _createCheckInsViewModel(),
            ViewType.AddRoom => _createRoomViewModal(),
            ViewType.Staff => _createStaffViewModel(),
            ViewType.Tasks => _createTasksViewModel(),
            _ => throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType")
        };
    }
}