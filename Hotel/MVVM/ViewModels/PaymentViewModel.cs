using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using Hotel.Application.DTOS.PaymentDto;
using Hotel.Application.DTOS.ReservationListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.Commands;
using Hotel.Domain.Entities;
using Hotel.Stores;

namespace Hotel.MVVM.ViewModels;

//TODO block UI when user is choosing RESERVATION, also have to trigger IsChecked when user switches view using messenger for better UX
public class PaymentViewModel : ViewModelBase, IRecipient<ReservationDto>
{
    private readonly IPaymentService _paymentService;
    
    private DateTime _checkInDate;

    private DateTime _checkOutDate;

    private string _floorAndRoomNumber;

    private string _guestFullname;

    private string _totalCost;

    private int _reservationId;
    
    private int _selectedPaymentTypeId;
    
    private DateTime _paymentDate = DateTime.Now;

    private ObservableCollection<PaymentListingDto> _paymentListingCollection;

    public PaymentViewModel(MessengerCurrentViewStorage messengerCurrentViewStorage,IPaymentService paymentService)
    {
        _paymentService = paymentService;
        
        TestCommand = new TestCommand("Open", this, messengerCurrentViewStorage);
        PayForReservationCommand = new PayForReservationCommand(this, paymentService);

        GetAllPaymentTypes();
        GetListingPayments();
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
    
    public int ReservationId
    {
        get { return _reservationId; }
        set
        {
            _reservationId = value;
            OnPropertyChanged(nameof(ReservationId));
        }
    }
    
    public int SelectedPaymentTypeId
    {
        get { return _selectedPaymentTypeId; }
        set
        {
            _selectedPaymentTypeId = value;
            if (_selectedPaymentTypeId == 3)
            {
                PaymentDateEnabled = true;
                PaymentDate = DateTime.Now.AddDays(14);
            }
            else
            {
                PaymentDate = DateTime.Now;
                PaymentDateEnabled = false;
            }
            OnPropertyChanged(nameof(PaymentDateEnabled));
            OnPropertyChanged(nameof(SelectedPaymentTypeId));
        }
    }
    
    public DateTime PaymentDate
    {
        get { return _paymentDate; }
        set
        {
            _paymentDate = value;
            OnPropertyChanged(nameof(PaymentDate));
        }
    }
    
    public ObservableCollection<PaymentListingDto> PaymentListingCollection
    {
        get { return _paymentListingCollection; }
        set
        {
            _paymentListingCollection = value;
            OnPropertyChanged(nameof(PaymentListingCollection));
        }
    }

    public bool PaymentDateEnabled { get; set; } = false;

    public IQueryable<PaymentType> PaymentTypes { get; set; }
    public ICommand PayForReservationCommand { get; }
    public ICommand TestCommand { get; }

    public void Receive(ReservationDto message)
    {
        ReservationId = message.ReservationId;
        CheckInDate = message.CheckInDate;
        CheckOutDate = message.CheckOutDate;
        TotalCost = message.TotalCost;
        GuestFullname = message.GuestFullName;
        FloorAndRoomNumber = message.FloorAndRoomNumber;

        WeakReferenceMessenger.Default.Send<string>("Close");
        WeakReferenceMessenger.Default.UnregisterAll(this);
    }

    public void RegisterPaymentViewModel()
    {
        WeakReferenceMessenger.Default.Register(this);
    }

    private void GetAllPaymentTypes()
    {
        PaymentTypes = _paymentService.GetPaymentTypes().AsQueryable();
    }

    private void GetListingPayments()
    {
        var allPayments = _paymentService.GetAllPayments();
        PaymentListingCollection = new ObservableCollection<PaymentListingDto>(allPayments);
    }
}