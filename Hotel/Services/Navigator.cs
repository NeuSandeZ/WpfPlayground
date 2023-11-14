using System;
using Hotel.MVVM.ViewModels;
using Hotel.Services.Interfaces;

namespace Hotel.Services;

public class Navigator : INavigator
{
    private ViewModelBase _currentModalViewModel;
    private ViewModelBase _currentViewModel;

    private bool _isModalOpen;

    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel?.Dispose();

            _currentViewModel = value;
            StateChanged?.Invoke();
        }
    }

    public ViewModelBase CurrentModalViewModel
    {
        get => _currentModalViewModel;
        set
        {
            _currentModalViewModel?.Dispose();

            _currentModalViewModel = value;
            StateModalChanged?.Invoke();
        }
    }

    public bool IsModalOpen => CurrentModalViewModel != null;


    public void Close()
    {
        CurrentModalViewModel = null;
    }

    public event Action StateModalChanged;
    public event Action StateChanged;
}