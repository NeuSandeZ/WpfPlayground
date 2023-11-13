using System.Windows.Input;
using Hotel.Commands;
using Hotel.Factories;
using Hotel.Services;
using Hotel.Stores;

namespace Hotel.MVVM.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly NavigationModalViewStore _navigationModalViewStore;
    private readonly INavigator _navigator;
    private readonly IViewModelFactory _viewModelFactory;
    
    public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;
    public ViewModelBase CurrentModalViewModel => _navigationModalViewStore.CurrentViewModel;
    public bool IsModalOpen => _navigationModalViewStore.IsOpenModal;
    
    public ICommand AddViewModalCommand { get; }
    public ICommand UpdateCurrentViewModelCommand { get; }
    
    public MainWindowViewModel(NavigationModalViewStore navigationModalViewStore,
        INavigator navigator, IViewModelFactory viewModelFactory)
    {
        _navigator = navigator;
        _viewModelFactory = viewModelFactory;
        _navigationModalViewStore = navigationModalViewStore;
        
        _navigator.StateChanged += OnCurrentViewModelChanged;
        _navigationModalViewStore.CurrentViewModelChanged += OnCurrentViewModalChanged;
        
        UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, _viewModelFactory);
        UpdateCurrentViewModelCommand.Execute(ViewType.Reservation);
        
        AddViewModalCommand = new NavigateModalCommand(_navigationModalViewStore,
            () => new CrudAddModalViewModel(_navigationModalViewStore));
    }
    
    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }
    
    private void OnCurrentViewModalChanged()
    {
        OnPropertyChanged(nameof(CurrentModalViewModel));
        OnPropertyChanged(nameof(IsModalOpen));
    }
    
    // public override void Dispose()
    // {
    //     _navigator.StateChanged -= OnCurrentViewModelChanged;
    //     base.Dispose();
    // }
}