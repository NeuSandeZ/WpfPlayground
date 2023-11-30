using Hotel.Application.DTOS.PaymentDto;
using Hotel.Application.Services.Interfaces;
using Hotel.MVVM.ViewModels;

namespace Hotel.Commands;

public class PayForReservationCommand : BaseCommand
{
    private readonly PaymentViewModel _paymentViewModel;
    private readonly IPaymentService _paymentService;
    public PayForReservationCommand(PaymentViewModel paymentViewModel, IPaymentService paymentService)
    {
        _paymentViewModel = paymentViewModel;
        _paymentService = paymentService;

    }
    public override void Execute(object? parameter)
    {
        var paymentDto = new PaymentDto()
        {
            PaymentDate = _paymentViewModel.PaymentDate,
            Amount =  int.Parse(_paymentViewModel.TotalCost.Replace("$","")),
            PaymentTypeId = _paymentViewModel.SelectedPaymentTypeId,
            ReservationId = _paymentViewModel.ReservationId
        };
        _paymentService.PayFor(paymentDto);
    }
}