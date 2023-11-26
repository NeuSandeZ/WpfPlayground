using System;
using CommunityToolkit.Mvvm.Messaging;
using Hotel.MVVM.ViewModels;
using Hotel.Services.Interfaces;

namespace Hotel.Commands;

public class TestCommand : BaseCommand
{
    private readonly string _switchCase;
    private readonly PaymentViewModel _paymentViewModel;

    public TestCommand(string switchCase, PaymentViewModel paymentViewModel)
    {
        _switchCase = switchCase;
        _paymentViewModel = paymentViewModel;
    }
    public override void Execute(object? parameter)
    {
        _paymentViewModel.RegisterMessenger();
        WeakReferenceMessenger.Default.Send(_switchCase);
    }
}