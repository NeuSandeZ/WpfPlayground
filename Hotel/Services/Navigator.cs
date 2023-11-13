using System;
using Hotel.MVVM.ViewModels;
using Hotel.Services.Interfaces;

namespace Hotel.Services;

public class Navigator : INavigator
{
    private ViewModelBase _currentViewModel;
    public ViewModelBase CurrentViewModel
    {
        get
        {
            return _currentViewModel;
        }
        set
        {
            _currentViewModel?.Dispose();
            
            _currentViewModel = value;
            StateChanged?.Invoke();
        }
    }
    
    public event Action StateChanged;
}