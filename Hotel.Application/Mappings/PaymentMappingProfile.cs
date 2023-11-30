using AutoMapper;
using Hotel.Application.DTOS.PaymentDto;
using Hotel.Domain.Entities;

namespace Hotel.Application.Mappings;

public class PaymentMappingProfile : Profile
{
    public PaymentMappingProfile()
    {
        CreateMap<PaymentDto, Payment>();
        
        CreateMap<Payment, PaymentListingDto>()
            .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentType.PaymentMethod))
            .ForMember(dest => dest.ReservationNumber, opt => opt.MapFrom(src => src.Reservation.ReservationNumber))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount)) //.ToString("C")
            .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => src.PaymentDate));  //.ToString("yyyy-MM-dd")
    }
}