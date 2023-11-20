using System.Windows.Input;
using Hotel.Commands;
using Hotel.Services.Interfaces;

namespace Hotel.MVVM.ViewModels;

public class AddGuestViewModel : ViewModelBase
{
    public AddGuestViewModel(INavigator navigator)
    {
        // AddReservationCommand = new AddReservationCommand(navigator);
    }

    public ICommand CloseModal { get; }
}