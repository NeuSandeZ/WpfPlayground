using System;
using Hotel.MVVM.ViewModels;

namespace Hotel.Services.Interfaces;

public enum ViewType
{
    Reservation,
    Guest,
    Rooms,
    Payments,
    AddCrud,
    AddGuest
}

public interface INavigator : INavigatorModal
{
    // ViewModelBase TemporaryViewModel { get; set; }
    ViewModelBase CurrentViewModel { get; set; }
    event Action StateChanged;
}