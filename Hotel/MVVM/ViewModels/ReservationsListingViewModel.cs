using System;
using System.Windows;
using System.Windows.Input;
using Hotel.Commands;
using Hotel.Infrastructure;
using Hotel.MVVM.Views;
using Hotel.Stores;

namespace Hotel.MVVM.ViewModels;

public class ReservationsListingViewModel : ViewModelBase
{
    public ICommand AddViewModalCommand { get; }
    public ReservationsListingViewModel(NavigationModalViewStore navigationModalViewStore)
    {
        AddViewModalCommand = new NavigateModalCommand(navigationModalViewStore,
            () => new CrudAddModalViewModel(navigationModalViewStore));
    }
}