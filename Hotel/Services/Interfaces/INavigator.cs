using System;
using Hotel.MVVM.ViewModels;

namespace Hotel.Services.Interfaces;

public enum ViewType
{
   Reservation,
   Test,
   TextXD,
   AddCrud
}

public interface INavigator : INavigatorModal
{
    ViewModelBase CurrentViewModel { get; set; }
    event Action StateChanged;
}