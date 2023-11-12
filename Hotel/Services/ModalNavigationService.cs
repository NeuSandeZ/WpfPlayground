using System;
using Hotel.MVVM.ViewModels;
using Hotel.Services.Interfaces;
using Hotel.Stores;

namespace Hotel.Services;

public class ModalNavigationService<TViewModel> : INavigationService where TViewModel : ViewModelBase
{
    private readonly NavigationModalViewStore _navigationModalViewStore;
    private readonly Func<TViewModel> _createViewModel;

    public ModalNavigationService(NavigationModalViewStore navigationModalViewStore, Func<TViewModel> createViewModel)
    {
        _navigationModalViewStore = navigationModalViewStore;
        _createViewModel = createViewModel;
    }

    public void Navigate()
    {
        _navigationModalViewStore.CurrentViewModel = _createViewModel();
    }
}