using System;
using Hotel.MVVM.ViewModels;

namespace Hotel.Services.Interfaces;

public interface INavigatorModal
{
    ViewModelBase CurrentModalViewModel { get; set; }
    bool IsModalOpen { get; }
    void Close();
    event Action StateModalChanged;
}