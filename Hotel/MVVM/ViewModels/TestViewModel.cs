using System.Windows.Input;
using Hotel.Commands;
using Hotel.Stores;

namespace Hotel.MVVM.ViewModels;

public class TestViewModel : ViewModelBase
{
    public ICommand AddModalCommand { get; }

    public TestViewModel(NavigationViewStore navigationViewStore)
    {
        AddModalCommand = new AddModalCommand();
    }
}