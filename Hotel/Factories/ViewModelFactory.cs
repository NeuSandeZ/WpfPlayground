using System;
using Hotel.MVVM.ViewModels;
using Hotel.Services;

namespace Hotel.Factories;

public class ViewModelFactory : IViewModelFactory
{
    private readonly CreateViewModel<ReservationsListingViewModel> _createReservationViewModel;
    private readonly CreateViewModel<TestViewModel> _createTestViewModel;

    public ViewModelFactory(CreateViewModel<TestViewModel> createTestViewModel,
        CreateViewModel<ReservationsListingViewModel> createReservationViewModel)
    {
        _createTestViewModel = createTestViewModel;
        _createReservationViewModel = createReservationViewModel;
    }

    public ViewModelBase CreateViewModel(ViewType viewType)
    {
        switch (viewType)
        {
            case ViewType.Reservation:
                return _createReservationViewModel();
            case ViewType.Test:
                return _createTestViewModel();
            default:
                throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType");
        }
    }
}