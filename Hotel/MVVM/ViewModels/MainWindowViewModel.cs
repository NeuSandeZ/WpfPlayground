using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using Hotel.Commands;
using Hotel.Factories;
using Hotel.Services.Interfaces;
using Hotel.Stores;

namespace Hotel.MVVM.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly INavigator _navigator;
    private readonly IViewModelFactory _viewModelFactory;

    public MainWindowViewModel(INavigator navigator, IViewModelFactory viewModelFactory)
    {
        _navigator = navigator;
        _viewModelFactory = viewModelFactory;

        _navigator.StateChanged += OnCurrentViewModelChanged;
        _navigator.StateModalChanged += OnCurrentViewModalChanged;
        
        UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, viewModelFactory);
        UpdateCurrentViewModelCommand.Execute(ViewType.Reservation);
    }

    public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;
    public ViewModelBase CurrentModalViewModel => _navigator.CurrentModalViewModel;
    public bool IsModalOpen => _navigator.IsModalOpen;
    public ICommand UpdateCurrentViewModelCommand { get; }

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }

    private void OnCurrentViewModalChanged()
    {
        OnPropertyChanged(nameof(CurrentModalViewModel));
        OnPropertyChanged(nameof(IsModalOpen));
    }

    public override void Dispose()
    {
        _navigator.StateChanged -= OnCurrentViewModelChanged;
        _navigator.StateModalChanged -= OnCurrentViewModalChanged;
        base.Dispose();
    }
}