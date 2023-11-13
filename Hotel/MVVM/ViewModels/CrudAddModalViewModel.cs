using System.Windows.Input;
using Hotel.Commands;
using Hotel.Stores;

namespace Hotel.MVVM.ViewModels;

public class CrudAddModalViewModel : ViewModelBase
{
    private readonly NavigationModalViewStore _navigationModalViewStore;
    public ICommand Something { get; }

    public CrudAddModalViewModel(NavigationModalViewStore navigationModalViewStore)
    {
        Something = new Something(Close);
        _navigationModalViewStore = navigationModalViewStore;
    }

    private void Close()
    {
        _navigationModalViewStore.Close();
    }
}