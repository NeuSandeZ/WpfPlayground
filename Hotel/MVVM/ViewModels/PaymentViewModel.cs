using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using Hotel.Application.DTOS.ReservationListingDto;
using Hotel.Commands;
using Hotel.Stores;

namespace Hotel.MVVM.ViewModels;

public class PaymentViewModel : ViewModelBase, IRecipient<ReservationDto>
{
    private DateTime _checkInDate;

    private DateTime _checkOutDate;

    private string _floorAndRoomNumber;

    private string _guestFullname;

    private string _totalCost;

    public PaymentViewModel(MessengerCurrentViewStorage messengerCurrentViewStorage)
    {
        TestCommand = new TestCommand("Open", this, messengerCurrentViewStorage);
    }

    public DateTime CheckInDate
    {
        get => _checkInDate;
        set
        {
            _checkInDate = value;
            OnPropertyChanged();
        }
    }

    public DateTime CheckOutDate
    {
        get => _checkOutDate;
        set
        {
            _checkOutDate = value;
            OnPropertyChanged();
        }
    }

    public string TotalCost
    {
        get => _totalCost;
        set
        {
            _totalCost = value;
            OnPropertyChanged();
        }
    }

    public string GuestFullname
    {
        get => _guestFullname;
        set
        {
            _guestFullname = value;
            OnPropertyChanged();
        }
    }

    public string FloorAndRoomNumber
    {
        get => _floorAndRoomNumber;
        set
        {
            _floorAndRoomNumber = value;
            OnPropertyChanged();
        }
    }

    public ICommand TestCommand { get; }

    public void Receive(ReservationDto message)
    {
        CheckInDate = message.CheckInDate;
        CheckOutDate = message.CheckOutDate;
        TotalCost = message.TotalCost.ToString();
        GuestFullname = message.GuestFullName;
        FloorAndRoomNumber = message.FloorAndRoomNumber;

        WeakReferenceMessenger.Default.Send<string>("Close");
        WeakReferenceMessenger.Default.UnregisterAll(this);
    }

    public void RegisterPaymentViewModel()
    {
        WeakReferenceMessenger.Default.Register(this);
    }
}