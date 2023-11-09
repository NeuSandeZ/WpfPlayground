using System;
using System.Windows;
using System.Windows.Input;
using Hotel.Commands;
using Hotel.Infrastructure;
using Hotel.MVVM.Views;
using Hotel.Services.Interfaces;
using Hotel.Stores;

namespace Hotel.MVVM.ViewModels;

public class ReservationsListingViewModel : ViewModelBase
{
    public ICommand AddModalCommand { get; }
    public ReservationsListingViewModel()
    {
        AddModalCommand = new AddModalCommand();
    }
}