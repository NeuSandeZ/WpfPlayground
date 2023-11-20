using AutoMapper;
using Hotel.Application.ReservationListingDto;
using Hotel.Domain.Entities;

namespace Hotel.Application.Mappings;

public class ReservationMappingProfile : Profile
{
    public ReservationMappingProfile()
    {
        CreateMap<Domain.Entities.Reservation, ReservationDto>()
            .ForMember(a => a.GuestFullName,
                c => c.MapFrom(src =>
                    $"{src.Guest.FirstName + " " + src.Guest.LastName}"
                ))
            .ForMember(a => a.FloorAndRoomNumber,
                c =>
                    c.MapFrom(src => $"{src.Room.FloorNumber + "" + src.Room.RoomNumber}"));

        

        CreateMap<Room, AvailableRoomsDto>()
            .ForMember(dest => dest.RoomId,
                opt => opt.MapFrom(src =>
                    src.Id))
            .ForMember(dest => dest.RoomNumber,
                opt => opt.MapFrom(src =>
                    src.RoomNumber))
            .ForMember(dest => dest.FloorNumber,
                opt => opt.MapFrom(src =>
                    src.FloorNumber));
            
        
        CreateMap<AddReservationDto, Reservation>()
            .ForMember(dest => dest.CheckInDate,
                opt => opt.MapFrom(src => src.CheckInDate))
            .ForMember(dest => dest.CheckOutDate
                , opt => opt.MapFrom(src => src.CheckOutDate))
            .ForMember(dest => dest.TotalCost,
                opt => opt.MapFrom(src => string.IsNullOrEmpty(src.TotalCost) ? (int?)null : int.Parse(src.TotalCost)))
            .ForMember(dest => dest.Guest,
                opt => opt.MapFrom(src => new Guest
            {
                FirstName = src.FirstName,
                LastName = src.LastName
            }))
            .ForMember(dest => dest.RoomId, 
                opt =>
                    opt.MapFrom(src => src.RoomId
            ))
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());
    }
}