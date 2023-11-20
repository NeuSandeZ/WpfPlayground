using AutoMapper;
using Hotel.Application.DTOS.GuestsListingDto;
using Hotel.Domain.Entities;

namespace Hotel.Application.Mappings;

public class GuestMappingProfile : Profile
{
    public GuestMappingProfile()
    {
        CreateMap<Guest, GuestDto>()
            .ForMember(dest => dest.City,
                membo => membo.MapFrom(
                    src => src.Address.City))
            .ForMember(dest => dest.Street,
                membo => membo.MapFrom(
                    src => src.Address.Street))
            .ForMember(dest => dest.PostalCode,
                membo => membo.MapFrom(
                    src => src.Address.PostalCode));

        CreateMap<GuestDto, Guest>()
            .ForMember(dest => dest.Address, opt =>
                opt.MapFrom(src => new Address()
                {
                    City = src.City,
                    Street = src.Street,
                    PostalCode = src.PostalCode
                }));
    }
}