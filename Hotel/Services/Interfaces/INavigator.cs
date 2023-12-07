using System;
using Hotel.MVVM.ViewModels;

namespace Hotel.Services.Interfaces;

public enum ViewType
{
    Reservation,
    Guest,
    Rooms,
    Payments,
    CheckInsOuts,
    AddCrud,
    AddGuest,
    AddRoom,
    Staff,
    Tasks,
    AddStaff,
    AddTask
}

public interface INavigator : INavigatorModal
{
    ViewModelBase CurrentViewModel { get; set; }
    event Action StateChanged;
}