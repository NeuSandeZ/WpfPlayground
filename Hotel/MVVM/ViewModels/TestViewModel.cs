using System.Windows.Input;
using Hotel.Commands;
using Hotel.Stores;

namespace Hotel.MVVM.ViewModels;

public class TestViewModel : ViewModelBase
{
    private readonly NavigationModalViewStore _navigationModalViewStore;
    public ViewModelBase CurrentModalViewModel => _navigationModalViewStore.CurrentViewModel;
    public bool IsModalOpen => _navigationModalViewStore.IsOpenModal;
    public ICommand AddViewModalCommand { get; }
    public TestViewModel(NavigationModalViewStore navigationModalViewStore)
    {
        _navigationModalViewStore = navigationModalViewStore;
        
        AddViewModalCommand = new NavigateModalCommand(_navigationModalViewStore,
            () => new TextXDViewModel());
        
        _navigationModalViewStore.CurrentViewModelChanged += OnCurrentViewModalChanged;
    }

    private void OnCurrentViewModalChanged()
    {
        OnPropertyChanged(nameof(CurrentModalViewModel));
        OnPropertyChanged(nameof(IsModalOpen));
    }
}