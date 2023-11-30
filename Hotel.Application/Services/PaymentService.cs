using System.Collections;
using AutoMapper;
using Hotel.Application.DTOS.PaymentDto;
using Hotel.Application.Services.Interfaces;
using Hotel.Domain.Entities;
using Hotel.Domain.IRepositories;

namespace Hotel.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;
    
    public PaymentService(IPaymentRepository paymentRepository, IMapper mapper)
    {
        _paymentRepository = paymentRepository;
        _mapper = mapper;
    }
    
    public void PayFor(PaymentDto paymentDto)
    {
        var payment = _mapper.Map<Payment>(paymentDto);
        _paymentRepository.PayFor(payment);
    }

    public IEnumerable<PaymentType> GetPaymentTypes()
    {
         return _paymentRepository.GetPaymentTypes();
    }
    public IEnumerable<PaymentListingDto> GetAllPayments()
    {
        var allPayments = _paymentRepository.GetAllPayments();
        return _mapper.Map< IEnumerable<PaymentListingDto>>(allPayments);
    }
}