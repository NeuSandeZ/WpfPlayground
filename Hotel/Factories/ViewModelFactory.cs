using System;
using Hotel.MVVM.ViewModels;
using Hotel.Services.Interfaces;

namespace Hotel.Factories;

public class ViewModelFactory : IViewModelFactory
{
    private readonly CreateViewModel<ReservationsListingViewModel> _createReservationViewModel;
    private readonly CreateViewModel<TestViewModel> _createTestViewModel;
    

    private readonly CreateViewModel<CrudAddModalViewModel> _createAddModalView;
    private readonly CreateViewModel<TextXDViewModel> _creatTestXDViewModel;
    
    public ViewModelFactory(CreateViewModel<TestViewModel> createTestViewModel,
        CreateViewModel<ReservationsListingViewModel> createReservationViewModel,
        CreateViewModel<CrudAddModalViewModel> createAddModalView, 
        CreateViewModel<TextXDViewModel> creatTestXdViewModel)
    {
        _createTestViewModel = createTestViewModel;
        _createReservationViewModel = createReservationViewModel;
        _createAddModalView = createAddModalView;
        _creatTestXDViewModel = creatTestXdViewModel;
    }

    public ViewModelBase CreateViewModel(ViewType viewType)
    {
        switch (viewType)
        {
            case ViewType.Reservation:
                return _createReservationViewModel();
            case ViewType.Test:
                return _createTestViewModel();
            case ViewType.TextXD:
                return _creatTestXDViewModel();
            case ViewType.AddCrud:
                return _createAddModalView();
            default:
                throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType");
        }
    }
}