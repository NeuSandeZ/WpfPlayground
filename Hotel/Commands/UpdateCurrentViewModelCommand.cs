using System;
using System.Windows.Input;
using Hotel.Factories;
using Hotel.Services.Interfaces;

namespace Hotel.Commands;

public class UpdateCurrentViewModelCommand : BaseCommand
{
    private readonly INavigator _navigator;
    private readonly IViewModelFactory _viewModelFactory;

    public UpdateCurrentViewModelCommand(INavigator navigator, IViewModelFactory viewModelFactory)
    {
        _navigator = navigator;
        _viewModelFactory = viewModelFactory;
    }
    
    public override void Execute(object? parameter)
    {
        if(parameter is ViewType)
        {
            ViewType viewType = (ViewType)parameter;

            _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
        }
    }
    
}