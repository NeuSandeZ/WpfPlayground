using System;
using System.ComponentModel;
using Hotel.Application.DTOS.CheckInsOutsDto;
using Hotel.Application.Services.Interfaces;
using Hotel.MVVM.ViewModels;

namespace Hotel.Commands;

public class CheckOutCommand : BaseCommand
{
    private readonly ICheckInOutService _checkInOutService;
    private readonly CheckInsOutsViewModel _selectedCheckOut;

    public CheckOutCommand(ICheckInOutService checkInOutService, CheckInsOutsViewModel selectedCheckOut)
    {
        _checkInOutService = checkInOutService;
        _selectedCheckOut = selectedCheckOut;

        _selectedCheckOut.PropertyChanged += OnModelPropertyChanged;
    }

    public override void Execute(object? parameter)
    {
        var checkOut = new CheckOutDto
        {
            CheckInId = _selectedCheckOut.SelectedCheckIn.CheckInId,
            CheckOutDate = DateTime.Now
        };

        _checkInOutService.CheckOut(checkOut);
        _selectedCheckOut.PropertyChanged -= OnModelPropertyChanged;
    }

    public override bool CanExecute(object? parameter)
    {
        return _selectedCheckOut.SelectedCheckIn != null && base.CanExecute(parameter);
    }

    private void OnModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(CheckInsOutsViewModel.SelectedCheckIn)) OnCanExecutedChanged();
    }
}