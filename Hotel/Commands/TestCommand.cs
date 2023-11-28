using CommunityToolkit.Mvvm.Messaging;
using Hotel.MVVM.ViewModels;
using Hotel.Stores;

namespace Hotel.Commands;

public class TestCommand : BaseCommand
{
    private readonly MessengerCurrentViewStorage _messengerCurrentViewStorage;
    private readonly PaymentViewModel _paymentViewModel;
    private readonly string _switchCase;

    public TestCommand(string switchCase, PaymentViewModel paymentViewModel,
        MessengerCurrentViewStorage messengerCurrentViewStorage)
    {
        _switchCase = switchCase;
        _paymentViewModel = paymentViewModel;
        _messengerCurrentViewStorage = messengerCurrentViewStorage;
    }

    public override void Execute(object? parameter)
    {
        _messengerCurrentViewStorage.RegisterMessengerCurrentViewStorage();
        _paymentViewModel.RegisterPaymentViewModel();
        WeakReferenceMessenger.Default.Send(_switchCase);
    }
}