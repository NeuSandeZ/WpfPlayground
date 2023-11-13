using System;
using System.Windows;
using System.Windows.Input;
using Hotel.Commands;
using Hotel.Factories;
using Hotel.Infrastructure;
using Hotel.MVVM.Views;
using Hotel.Services.Interfaces;

namespace Hotel.MVVM.ViewModels;

public class ReservationsListingViewModel : ViewModelBase
{
    private readonly INavigator _navigator;
    public ICommand OpenModal { get; }
    
    public ReservationsListingViewModel(INavigator navigator, IViewModelFactory viewModelFactory)
    {
        _navigator = navigator;

        OpenModal = new OpenModalCommand(navigator, viewModelFactory, () => ViewType.AddCrud);
    }
}