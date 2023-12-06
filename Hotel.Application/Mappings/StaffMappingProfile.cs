using AutoMapper;
using Hotel.Application.DTOS.StaffListingDto;
using Hotel.Domain.Entities;

namespace Hotel.Application.Mappings;

public class StaffMappingProfile : Profile
{
    public StaffMappingProfile()
    {
        CreateMap<StaffRole, RolesDto>();
        
        CreateMap<Staff, StaffListingDto>()
            .ForMember(dest=> dest.Role,
                opt=> opt.MapFrom(
                    src=> src.StaffRole.Role))
            .ForMember(dest => dest.City,
                membo => membo.MapFrom(
                    src => src.Address.City))
            .ForMember(dest => dest.Street,
                membo => membo.MapFrom(
                    src => src.Address.Street))
            .ForMember(dest => dest.PostalCode,
                membo => membo.MapFrom(
                    src => src.Address.PostalCode));

        CreateMap<CreateStaffMemberDto, Staff>()
            .ForMember(dest => dest.Address,
                opt => opt.MapFrom(
                    src => new Address()
                    {
                       City = src.City,
                       Street = src.Street,
                       PostalCode = src.PostalCode
                    }));
    }
}