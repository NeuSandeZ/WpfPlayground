using System;
using Hotel.MVVM.ViewModels;
using Hotel.Services.Interfaces;

namespace Hotel.Services;

public class Navigator : INavigator
{
    private bool _isModalOpen;

    private ViewModelBase _currentViewModel;

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

    private ViewModelBase _currentModalViewModel;
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

    // private ViewModelBase _temporaryViewModel;
    // public ViewModelBase TemporaryViewModel
    // {
    //     get { return _temporaryViewModel; }
    //     set
    //     {
    //         _temporaryViewModel = value;
    //         StateChanged?.Invoke();
    //     }
    // }
    public void Close()
    {
        CurrentModalViewModel = null;
    }
    
    public event Action StateModalChanged;
    public event Action StateChanged;
}