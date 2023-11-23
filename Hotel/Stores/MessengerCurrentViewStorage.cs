using System;
using CommunityToolkit.Mvvm.Messaging;
using Hotel.Factories;
using Hotel.MVVM.ViewModels;
using Hotel.Services.Interfaces;

namespace Hotel.Stores;

public class MessengerCurrentViewStorage
{
    private readonly INavigator _navigator;
    private readonly IViewModelFactory _viewModelFactory;
    
    public MessengerCurrentViewStorage(INavigator navigator, IViewModelFactory viewModelFactory)
    {
        _navigator = navigator;
        _viewModelFactory = viewModelFactory;

        Console.WriteLine("Catching commands in messenger");
        WeakReferenceMessenger.Default.Register<string>(this,Open);
    }
    
    private ViewModelBase? TemporaryViewModel { get; set; }
    public bool IsTemporaryViewModelOpened => TemporaryViewModel != null;
    
    private void Open(object recipient, string message)
    {
        switch (message)
        {
            case "Open":
                TemporaryViewModel = _navigator.CurrentViewModel;
                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(ViewType.Reservation);
                Console.WriteLine("Open triggered");
                break;
            case "Close":
                _navigator.CurrentViewModel = TemporaryViewModel;
                TemporaryViewModel = null;
                Console.WriteLine("Close triggered");
                break;
        }
    }
}