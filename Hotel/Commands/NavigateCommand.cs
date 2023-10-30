using Hotel.MVVM.ViewModels;
using Hotel.Services.Interfaces;
using Hotel.Stores;

namespace Hotel.Commands;

public class NavigateCommand : BaseCommand
{
    private readonly INavigationService _navigationService;
    
    public NavigateCommand(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        if (parameter is View view)
        {
            _navigationService.Navigate(view); 
        }
    }
}