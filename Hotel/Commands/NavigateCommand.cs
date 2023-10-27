using Hotel.MVVM.ViewModels;
using Hotel.Services.Interfaces;
using Hotel.Stores;

namespace Hotel.Commands;

public class NavigateCommand : BaseCommand
{
    private readonly NavigationStore _navigationStore;
    private readonly INavigationService _navigationService;

    public NavigateCommand(NavigationStore navigationStore, INavigationService navigationService)
    {
        _navigationStore = navigationStore;
        _navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        if (parameter is View view)
        {
            _navigationStore.CurrentViewModel = _navigationService.Navigate(view); 
        }
    }
}