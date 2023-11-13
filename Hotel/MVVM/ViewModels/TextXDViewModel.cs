using System.Windows.Input;
using Hotel.Commands;
using Hotel.Stores;

namespace Hotel.MVVM.ViewModels;

public class TextXDViewModel : ViewModelBase
{
    private readonly NavigationModalViewStore _navigationModalViewStore;
    public ICommand Something { get; }

    public TextXDViewModel(NavigationModalViewStore navigationModalViewStore)
    {
        Something = new Something(Close);
        _navigationModalViewStore = navigationModalViewStore;
    }

    private void Close()
    {
        _navigationModalViewStore.Close();
    }
}