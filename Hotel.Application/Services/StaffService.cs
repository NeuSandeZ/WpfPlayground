using AutoMapper;
using Hotel.Application.DTOS.StaffListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.Domain.Entities;
using Hotel.Domain.IRepositories;

namespace Hotel.Application.Services;

public class StaffService : IStaffService
{
    private readonly IStaffRepository _staffRepository;
    private readonly IMapper _mapper;
    
    public StaffService(IStaffRepository staffRepository, IMapper mapper)
    {
        _staffRepository = staffRepository;
        _mapper = mapper;
    }

    public IEnumerable<RolesDto> GetAllStaffRoles()
    {
        var allStaffRoles = _staffRepository.GetAllStaffRoles();
        return _mapper.Map<IEnumerable<RolesDto>>(allStaffRoles);
    }

    public void CreateStaff(CreateStaffMemberDto staff)
    {
        var staffMember = _mapper.Map<Staff>(staff);
        _staffRepository.CreateStaff(staffMember);
    }

    public IEnumerable<StaffListingDto> GetAllStaffMembers()
    {
        var allStaffMembers = _staffRepository.GetAllStaffMembers();
        return _mapper.Map<IEnumerable<StaffListingDto>>(allStaffMembers);
    }
}