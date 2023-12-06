using AutoMapper;
using Hotel.Application.DTOS.CheckInsOutsDto;
using Hotel.Domain.Entities;

namespace Hotel.Application.Mappings;

public class CheckInsOutsMappingProfile : Profile
{
    public CheckInsOutsMappingProfile()
    {
        CreateMap<CheckInDto, CheckIns>();
        CreateMap<CheckIns, CheckInListingDto>()
            .ForMember(dest => dest.CheckInId,
                opt => opt.MapFrom(
                    src => src.Id))
            .ForMember(dest => dest.CheckOutDate,
                opt => opt.MapFrom(
                    src => src.CheckOuts.CheckOutDate))
            .ForMember(dest => dest.FloorAndRoomNumber,
                opt => opt.MapFrom(
                    src => $"{src.Room.FloorNumber +""+src.Room.RoomNumber} "))
            .ForMember(dest => dest.ReservationNumber,
                opt => opt.MapFrom(
                    src => src.Reservation.ReservationNumber.ToString()))
            .ForMember(dest => dest.FullGuestName,
                opt => opt.MapFrom(
                    src => $"{src.Guest.FirstName +" "+ src.Guest.LastName}"));
    }
}