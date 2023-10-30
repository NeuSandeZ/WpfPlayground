﻿using System.Windows.Input;
using Hotel.Commands;
using Hotel.Services.Interfaces;
using Hotel.Stores;

namespace Hotel.MVVM.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;

    public MainWindowViewModel(NavigationStore navigationStore, INavigationService navigationService)
    {
        _navigationStore = navigationStore;
        NavigateCommand = new NavigateCommand(navigationService);
        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
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