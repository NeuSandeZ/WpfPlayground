using System;
using Hotel.MVVM.ViewModels;

namespace Hotel.Services.Interfaces;

public enum ViewType
{
   Reservation,
   Test
}

public interface INavigator
{
    ViewModelBase CurrentViewModel { get; set; }
    event Action StateChanged;
}