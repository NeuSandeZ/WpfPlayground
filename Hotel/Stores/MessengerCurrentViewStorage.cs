using CommunityToolkit.Mvvm.Messaging;
using Hotel.Factories;
using Hotel.MVVM.ViewModels;
using Hotel.Services.Interfaces;

namespace Hotel.Stores;

public class MessengerCurrentViewStorage : IRecipient<string>
{
    private readonly INavigator _navigator;
    private readonly IViewModelFactory _viewModelFactory;

    public MessengerCurrentViewStorage(INavigator navigator, IViewModelFactory viewModelFactory)
    {
        _navigator = navigator;
        _viewModelFactory = viewModelFactory;

        // WeakReferenceMessenger.Default.Register(this);
    }

    private ViewModelBase? TemporaryViewModel { get; set; }
    public bool IsTemporaryViewModelOpened => TemporaryViewModel != null;

    public void Receive(string message)
    {
        switch (message)
        {
            case "Open":
                TemporaryViewModel = _navigator.CurrentViewModel;
                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(ViewType.Reservation);
                break;
            case "Close":
                _navigator.CurrentViewModel = TemporaryViewModel;
                WeakReferenceMessenger.Default.UnregisterAll(this);
                TemporaryViewModel = null;
                break;
        }
    }

    public void RegisterMessengerCurrentViewStorage()
    {
        WeakReferenceMessenger.Default.Register(this);
    }
}