using AutoMapper;
using Hotel.Application.DTOS.ReservationListingDto;
using Hotel.Domain.Entities;

namespace Hotel.Application.Mappings;

public class ReservationMappingProfile : Profile
{
    public ReservationMappingProfile()
    {
        CreateMap<Reservation, ReservationDto>()
            .ForMember(a => a.ReservationId,
                c =>
                    c.MapFrom(src => src.Id))
            .ForMember(a => a.GuestFullName,
                c => c.MapFrom(src =>
                    $"{src.Guest.FirstName + " " + src.Guest.LastName}"
                ))
            .ForMember(a => a.FloorAndRoomNumber,
                c =>
                    c.MapFrom(src => $"{src.Room.FloorNumber + "" + src.Room.RoomNumber}"))
            .ForMember(a => a.ReservationStatus,
                c =>
                    c.MapFrom(src => src.ReservationStatus.Status))
            .ForMember(a => a.TotalCost,
                c =>
                    c.MapFrom(src => src.TotalCost));


        CreateMap<Room, AvailableRoomsDto>()
            .ForMember(dest => dest.RoomId,
                opt => opt.MapFrom(src =>
                    src.Id))
            .ForMember(dest => dest.RoomNumber,
                opt => opt.MapFrom(src =>
                    src.RoomNumber))
            .ForMember(dest => dest.FloorNumber,
                opt => opt.MapFrom(src =>
                    src.FloorNumber))
            .ForMember(dest => dest.PricePerNight,
                opt => opt.MapFrom(src =>
                    src.PricePerNight));

        
        CreateMap<AddReservationDto, Reservation>()
            .ForMember(dest => dest.CheckInDate,
                opt => opt.MapFrom(src => src.CheckInDate))
            .ForMember(dest => dest.CheckOutDate
                , opt => opt.MapFrom(src => src.CheckOutDate))
            .ForMember(dest => dest.TotalCost,
                opt => opt.MapFrom(src => string.IsNullOrEmpty(src.TotalCost) ? (int?)null : int.Parse(src.TotalCost.Replace("$",""))))
            .ForMember(dest => dest.GuestId,
                opt => opt.MapFrom(src => src.GuestId))
            .ForMember(dest => dest.RoomId,
                opt =>
                    opt.MapFrom(src => src.RoomId
                    ))
            .ForMember(dest => dest.ReservationStatusId,
                opt => opt.MapFrom(
                    src => src.ReservationStatusId))
            .ForMember(dest => dest.ReservationNumber,
                opt => opt.MapFrom(
                    src => src.ReservationNumber))
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());
    }
}