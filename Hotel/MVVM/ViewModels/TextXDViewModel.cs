using System.Windows.Input;
using Hotel.Commands;
using Hotel.Services.Interfaces;

namespace Hotel.MVVM.ViewModels;

public class TextXDViewModel : ViewModelBase
{
    public ICommand CloseModal { get; }
    public TextXDViewModel(INavigator navigator)
    {
        CloseModal = new CloseModal(navigator);
    }
}