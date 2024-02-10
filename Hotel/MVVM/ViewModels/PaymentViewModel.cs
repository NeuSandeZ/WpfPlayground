using System;
using System.Collections.Generic;
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
public class PaymentViewModel : SortingAndFilteringViewModel<PaymentListingDto>, IRecipient<ReservationDto>
{
    private readonly IPaymentService _paymentService;

    private DateTime _checkInDate;

    private DateTime _checkOutDate;

    private string _floorAndRoomNumber;

    private string _guestFullname;

    private DateTime _paymentDate = DateTime.Now;

    private int _reservationId;

    private int _selectedPaymentTypeId;

    private string _totalCost;

    public PaymentViewModel(MessengerCurrentViewStorage messengerCurrentViewStorage, IPaymentService paymentService)
    {
        _paymentService = paymentService;

        TestCommand = new TestCommand("Open", this, messengerCurrentViewStorage);
        PayForReservationCommand = new PayForReservationCommand(this, paymentService);
        

        GetAllPaymentTypes();
        GetListingPayments();

        FilterComboBoxList = new ObservableCollection<string>(LoadFilterComboBoxList());
        SortComboBoxList = new ObservableCollection<string>(LoadSortComboBoxList());

        Refresh = new ActionBaseCommand(GetListingPayments);
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
        get => _reservationId;
        set
        {
            _reservationId = value;
            OnPropertyChanged();
        }
    }

    public int SelectedPaymentTypeId
    {
        get => _selectedPaymentTypeId;
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
            OnPropertyChanged();
        }
    }

    public DateTime PaymentDate
    {
        get => _paymentDate;
        set
        {
            _paymentDate = value;
            OnPropertyChanged();
        }
    }

    public bool PaymentDateEnabled { get; set; }

    public IQueryable<PaymentType> PaymentTypes { get; set; }
    public ICommand PayForReservationCommand { get; }
    public ICommand TestCommand { get; }
    public ICommand Refresh { get; set; }

    protected override Dictionary<string, Func<PaymentListingDto, string>> FilterByColumn { get; } = new()
    {
        { "Amount", a => a.Amount },
        { "Payment date", a => a.PaymentDate },
        { "Payment method", a => a.PaymentMethod },
        { "Reservation number", a => a.ReservationNumber }
    };

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

    ~PaymentViewModel()
    {
        DisposeFilter();
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
        Items = new ObservableCollection<PaymentListingDto>(allPayments);
    }

    protected override List<string> LoadFilterComboBoxList()
    {
        return new List<string>
        {
            "Amount",
            "Payment date",
            "Payment method",
            "Reservation number"
        };
    }

    protected override void Sort()
    {
        CollectionView.SortDescriptions.Clear();

        var columnToSort = ChoosenSortField switch
        {
            "Amount" => nameof(PaymentListingDto.Amount),
            "Payment date" => nameof(PaymentListingDto.PaymentDate),
            "Payment method" => nameof(PaymentListingDto.PaymentMethod),
            "Reservation number" => nameof(PaymentListingDto.ReservationNumber),
            _ => null
        };
        SortByAscOrDesc(columnToSort);
    }
}