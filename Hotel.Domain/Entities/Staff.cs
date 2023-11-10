using System.ComponentModel.DataAnnotations;

namespace Hotel.Domain.Entities;

public class Staff 
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string FullName { get; set; } = default!;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public int? BankAccountNumber { get; set; }
    
    public bool? IsDeleted { get; set; }

    public StaffRole StaffRole { get; set; } = default!;
    public int StaffRoleId { get; set; } = default!;

    public Address Address { get; set; } = default!;
    public int AddressId { get; set; } = default!;
}