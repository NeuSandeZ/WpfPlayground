using System;
using Hotel.MVVM.ViewModels;
using Hotel.Stores;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Hotel.Commands;

public class NavigateModalCommand : BaseCommand

{
    private readonly NavigationModalViewStore _navigationModalViewStore;
    private readonly Func<ViewModelBase> _createViewModel;

    public NavigateModalCommand(NavigationModalViewStore navigationModalViewStore, Func<ViewModelBase> createViewModel)
    {
        _navigationModalViewStore = navigationModalViewStore;
        _createViewModel = createViewModel;
    }

    public override void Execute(object? parameter)
    {
        _navigationModalViewStore.CurrentViewModel = _createViewModel();
    }
}