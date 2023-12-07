using AutoMapper;
using Hotel.Application.DTOS.RoomsListingDto;
using Hotel.Domain.Entities;

namespace Hotel.Application.Mappings;

public class RoomMappingProfile : Profile
{
    public RoomMappingProfile()
    {
        CreateMap<Room, RoomsListingDto>()
            .ForMember(dest => dest.RoomNumber,
                opt => opt.MapFrom(
                    src => src.RoomNumber.ToString()))
            .ForMember(dest => dest.FloorNumber,
                opt => opt.MapFrom(
                    src => src.FloorNumber.ToString()))
            .ForMember(dest => dest.PricePerNight,
                opt => opt.MapFrom(
                    src => src.PricePerNight.ToString()))
            .ForMember(dest => dest.RoomPromotion,
                opt => opt.MapFrom(
                    src => src.RoomPromotions.DiscountAmount))
            .ForMember(dest => dest.RoomStatus,
                opt => opt.MapFrom(
                    src => src.RoomStatus.CurrentState))
            .ForMember(dest => dest.RoomType,
                opt => opt.MapFrom(
                    src => src.RoomType.Type));
        
        CreateMap<RoomType, RoomTypeDto>();
    }
}