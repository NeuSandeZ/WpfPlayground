using System.Windows.Input;
using Hotel.Commands;
using Hotel.Services.Interfaces;

namespace Hotel.MVVM.ViewModels.Modals;

public class AddGuestViewModel : ViewModelBase
{
    private readonly GuestViewModel _guestViewModel;
    public AddGuestViewModel(INavigator navigator, GuestViewModel guestViewModel)
    {
        _guestViewModel = guestViewModel;
        CloseModal = new CloseModalCommand(navigator);
    }

    public ICommand CloseModal { get; }
}