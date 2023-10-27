using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Hotel.Commands;
using Hotel.MVVM.ViewModels;
using Hotel.Services.Interfaces;
using Hotel.Stores;

namespace Hotel;

public class MainWindowViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;
    private readonly INavigationService _navigationService;
    public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
    public ICommand NavigateCommand { get; }
    public MainWindowViewModel(NavigationStore navigationStore, INavigationService navigationService)
    {
        _navigationStore = navigationStore;
        _navigationService = navigationService;
        NavigateCommand = new NavigateCommand(_navigationStore, _navigationService);
        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }
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