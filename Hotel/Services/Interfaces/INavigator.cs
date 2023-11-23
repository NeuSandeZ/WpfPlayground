using System;
using Hotel.MVVM.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Hotel.Services.Interfaces;

public enum ViewType
{
    Reservation,
    Guest,
    Rooms,
    Payments,
    TextXD,
    AddCrud,
}

public interface INavigator : INavigatorModal
{
    // ViewModelBase TemporaryViewModel { get; set; }
    ViewModelBase CurrentViewModel { get; set; }
    event Action StateChanged;
}