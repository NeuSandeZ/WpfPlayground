using System;
using Hotel.MVVM.ViewModels;
using Hotel.Stores;

namespace Hotel.Commands;

public class NavigateModalCommand<TViewModel> : BaseCommand
    where TViewModel : ViewModelBase
{
    private readonly NavigationModalViewStore _navigationModalViewStore;
    private readonly Func<TViewModel> _createViewModel;

    public NavigateModalCommand(NavigationModalViewStore navigationModalViewStore, Func<TViewModel> createViewModel)
    {
        _navigationModalViewStore = navigationModalViewStore;
        _createViewModel = createViewModel;
    }

    public override void Execute(object? parameter)
    {
        _navigationModalViewStore.CurrentViewModel = _createViewModel();
    }
}