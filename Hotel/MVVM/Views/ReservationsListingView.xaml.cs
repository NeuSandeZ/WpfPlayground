using System;
using System.Windows;
using Hotel.MVVM.ViewModels;

namespace Hotel.MVVM.Views;

public partial class ReservationsListingView : CrudViewBase
{
    private readonly ReservationsListingViewModel _reservationsListingViewModel;

    public ReservationsListingView(ReservationsListingViewModel reservationsListingViewModel)
    {
        _reservationsListingViewModel = reservationsListingViewModel;
        InitializeComponent();
    }

    protected override async void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);
        await _reservationsListingViewModel.LoadDataAsync();
    }
}