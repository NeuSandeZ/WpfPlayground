using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using Hotel.Application.DTOS.ReservationListingDto;
using Hotel.Commands;
using Hotel.Services.Interfaces;

namespace Hotel.MVVM.ViewModels;

public class PaymentViewModel : ViewModelBase
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
        Console.WriteLine("Payment view model");
        
        TestCommand = new TestCommand("Open");
        
        WeakReferenceMessenger.Default.Register<PaymentViewModel, ReservationDto>(this, (recipient, message) =>
        {
            CheckInDate = message.CheckInDate;
            CheckOutDate = message.CheckOutDate;

            Console.WriteLine("Register and sending close");
            WeakReferenceMessenger.Default.Send<string>("Close");
        });
         
    }
    
    public ICommand TestCommand { get; }
}