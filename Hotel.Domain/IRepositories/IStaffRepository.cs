using Hotel.Domain.Entities;

namespace Hotel.Domain.IRepositories;

public interface IStaffRepository
{
    IEnumerable<StaffRole> GetAllStaffRoles();
    void CreateStaff(Staff staff);
    IEnumerable<Staff> GetAllStaffMembers();
}