using System;
using Hotel.MVVM.ViewModels;
using Hotel.Stores;

namespace Hotel.Commands;

public class NavigateCommand<TViewModel> : BaseCommand
where TViewModel : ViewModelBase
{
    private readonly NavigationViewStore _navigationStore;
    private readonly Func<TViewModel> _createViewModel;

    public NavigateCommand(NavigationViewStore navigationStore, Func<TViewModel> createViewModel)
    {
        _navigationStore = navigationStore;
        _createViewModel = createViewModel;
    }

    public override void Execute(object? parameter)
    {
        _navigationStore.CurrentViewModel = _createViewModel();
    }
}