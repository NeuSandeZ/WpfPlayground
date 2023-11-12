using System;
using System.Windows;
using System.Windows.Input;
using Hotel.Commands;
using Hotel.Infrastructure;
using Hotel.MVVM.Views;
using Hotel.Stores;

namespace Hotel.MVVM.ViewModels;

public class ReservationsListingViewModel : ViewModelBase
{
    private readonly NavigationModalViewStore _navigationModalViewStore;
    public ViewModelBase CurrentModalViewModel => _navigationModalViewStore.CurrentViewModel;
    public bool IsModalOpen => _navigationModalViewStore.IsOpenModal;
    public ICommand AddViewModalCommand { get; }
    public ReservationsListingViewModel(NavigationModalViewStore navigationModalViewStore)
    {
        _navigationModalViewStore = navigationModalViewStore;
        
        AddViewModalCommand = new NavigateModalCommand(_navigationModalViewStore,
            () => new CrudAddModalViewModel());
        
        _navigationModalViewStore.CurrentViewModelChanged += OnCurrentViewModalChanged;
    }

    private void OnCurrentViewModalChanged()
    {
        OnPropertyChanged(nameof(CurrentModalViewModel));
        OnPropertyChanged(nameof(IsModalOpen));
    }
}