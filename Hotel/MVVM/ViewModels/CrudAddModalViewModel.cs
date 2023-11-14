using System.Windows.Input;
using Hotel.Commands;
using Hotel.Services.Interfaces;

namespace Hotel.MVVM.ViewModels;

public class CrudAddModalViewModel : ViewModelBase
{
    public CrudAddModalViewModel(INavigator navigator)
    {
        CloseModal = new CloseModal(navigator);
    }

    public ICommand CloseModal { get; }
}