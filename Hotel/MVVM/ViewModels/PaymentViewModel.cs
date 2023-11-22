using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using Hotel.Application.DTOS.ReservationListingDto;
using Hotel.Commands;

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
    public PaymentViewModel()
    {
        Console.WriteLine("In constructor!");

        TestCommand = new TestCommand();
        WeakReferenceMessenger.Default.Register<PaymentViewModel, ReservationDto>(this, (recipient, message) =>
        {
            CheckInDate = message.CheckInDate;

            WeakReferenceMessenger.Default.Send<string>("Close");
             
            Console.WriteLine("Message received!");
        });
         
    }
    
    public ICommand TestCommand { get; }
}