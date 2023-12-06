using Hotel.Domain.Entities;
using Hotel.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure.Repositories;

public class StaffRepository : IStaffRepository
{
    private readonly HotelDbContext _hotelDbContext;

    public StaffRepository(HotelDbContext hotelDbContext)
    {
        _hotelDbContext = hotelDbContext;
    }
    
    public IEnumerable<StaffRole> GetAllStaffRoles()
    {
        return _hotelDbContext.StaffRoles.ToList();
    }

    public void CreateStaff(Staff staff)
    {
        _hotelDbContext.Add(staff);
        _hotelDbContext.SaveChanges();
    }

    public IEnumerable<Staff> GetAllStaffMembers()
    {
        return _hotelDbContext.Staves.Select(a => new Staff()
            {
                FullName = a.FullName,
                Email = a.Email,
                PhoneNumber = a.PhoneNumber,
                Address = new Address()
                {
                    City = a.Address.City,
                    Street = a.Address.Street,
                    PostalCode = a.Address.PostalCode
                },
                StaffRole = new StaffRole()
                {
                    Role = a.StaffRole.Role
                }
            }).AsNoTracking()
            .ToList();
    }
}