using System.Windows.Input;
using Hotel.Commands;
using Hotel.Factories;
using Hotel.Services.Interfaces;

namespace Hotel.MVVM.ViewModels;

public class ReservationsListingViewModel : ViewModelBase
{
    private readonly INavigator _navigator;

    public ReservationsListingViewModel(INavigator navigator, IViewModelFactory viewModelFactory)
    {
        _navigator = navigator;

        OpenModal = new OpenModalCommand(navigator, viewModelFactory, () => ViewType.AddCrud);
    }

    public ICommand OpenModal { get; }
}