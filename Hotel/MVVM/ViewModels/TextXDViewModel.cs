using System.Windows.Input;
using Hotel.Commands;
using Hotel.Services.Interfaces;

namespace Hotel.MVVM.ViewModels;

public class TextXDViewModel : ViewModelBase
{
    public TextXDViewModel(INavigator navigator)
    {
        // AddReservationCommand = new AddReservationCommand(navigator);
    }

    public ICommand CloseModal { get; }
}