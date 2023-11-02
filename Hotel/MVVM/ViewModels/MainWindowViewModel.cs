using System.Windows.Input;
using Hotel.Commands;
using Hotel.Services.Interfaces;
using Hotel.Stores;

namespace Hotel.MVVM.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly NavigationViewStore _navigationViewStore;

    public MainWindowViewModel(NavigationViewStore navigationViewStore, INavigationService navigationService)
    {
        _navigationViewStore = navigationViewStore;
        NavigateCommand = new NavigateCommand(navigationService);
        _navigationViewStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    public ViewModelBase CurrentViewModel => _navigationViewStore.CurrentViewModel;
    public ICommand NavigateCommand { get; }

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }
}

public enum View
{
    ListingViewModel,
    TestViewModel
}