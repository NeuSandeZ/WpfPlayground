using Hotel.Application.DTOS.StaffListingDto;

namespace Hotel.Application.Services.Interfaces;

public interface IStaffService
{
    IEnumerable<RolesDto> GetAllStaffRoles();
    void CreateStaff(CreateStaffMemberDto staff);
    IEnumerable<StaffListingDto> GetAllStaffMembers();
}