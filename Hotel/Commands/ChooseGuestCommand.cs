using CommunityToolkit.Mvvm.Messaging;
using Hotel.MVVM.ViewModels;
using Hotel.MVVM.ViewModels.Modals;
using Hotel.Stores;

namespace Hotel.Commands;

public class ChooseGuestCommand : BaseCommand
{
    private readonly AddReservationViewModel _addReservationViewModel;
    private readonly MessengerCurrentViewStorage _messengerCurrentViewStorage;
    private readonly string _switchCase;

    public ChooseGuestCommand(string switchCase,
        MessengerCurrentViewStorage messengerCurrentViewStorage, AddReservationViewModel addReservationViewModel)
    {
        _switchCase = switchCase;
        _messengerCurrentViewStorage = messengerCurrentViewStorage;
        _addReservationViewModel = addReservationViewModel;
    }

    public override void Execute(object? parameter)
    {
        _messengerCurrentViewStorage.RegisterMessengerCurrentViewStorage();
        _addReservationViewModel.RegisterAddReservationViewModel();
        WeakReferenceMessenger.Default.Send(_switchCase);
    }
}