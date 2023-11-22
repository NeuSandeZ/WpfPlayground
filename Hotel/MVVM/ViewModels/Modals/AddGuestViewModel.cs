using System.Windows.Input;
using Hotel.Commands;
using Hotel.Services.Interfaces;

namespace Hotel.MVVM.ViewModels.Modals;

public class AddGuestViewModel : ViewModelBase
{
    public AddGuestViewModel(INavigator navigator)
    {
        CloseModal = new CloseModalCommand(navigator);
    }

    public ICommand CloseModal { get; }
}