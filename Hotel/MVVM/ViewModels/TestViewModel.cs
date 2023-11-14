using System.Windows.Input;
using Hotel.Commands;
using Hotel.Factories;
using Hotel.Services.Interfaces;

namespace Hotel.MVVM.ViewModels;

public class TestViewModel : ViewModelBase
{
    private readonly INavigator _navigator;

    public TestViewModel(INavigator navigator, IViewModelFactory viewModelFactory)
    {
        _navigator = navigator;

        OpenModal = new OpenModalCommand(navigator, viewModelFactory, () => ViewType.TextXD);
    }

    public ICommand OpenModal { get; }
}