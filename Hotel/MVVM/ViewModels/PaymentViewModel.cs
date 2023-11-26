using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using Hotel.Application.DTOS.ReservationListingDto;
using Hotel.Commands;
using Hotel.Services.Interfaces;
using Hotel.Stores;

namespace Hotel.MVVM.ViewModels;

public class PaymentViewModel : ViewModelBase, IRecipient<ReservationDto>
{
    private DateTime _checkInDate;
    public DateTime CheckInDate 
    {
        get { return _checkInDate; }
        set
        {
            _checkInDate = value;
            OnPropertyChanged(nameof(CheckInDate));
        }
    }

    private DateTime _checkOutDate;

    public DateTime CheckOutDate
    {
        get { return _checkOutDate; }
        set
        {
            _checkOutDate = value;
            OnPropertyChanged(nameof(CheckOutDate));
        }
    }
    public PaymentViewModel()
    {
        TestCommand = new TestCommand("Open", this);
    }
    public ICommand TestCommand { get; }
    public void Receive(ReservationDto message)
    {
        CheckInDate = message.CheckInDate;
        CheckOutDate = message.CheckOutDate;
        
        WeakReferenceMessenger.Default.Send<string>("Close");
        WeakReferenceMessenger.Default.UnregisterAll(this);
    }

    public void RegisterMessenger()
    {
        WeakReferenceMessenger.Default.Register(this);
    }
}