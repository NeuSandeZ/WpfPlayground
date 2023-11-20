using System.ComponentModel.DataAnnotations;

namespace Hotel.Domain.Entities;

public class Guest
{
    [Key] public int Id { get; set; }
    [Required] public string FirstName { get; set; } = default!;
    [Required] public string LastName { get; set; } = default!;
    public string? Email { get; set; } = default!;
    public string? PhoneNumber { get; set; } = default!;
    public Address? Address { get; set; } = default!;
    public int? AddressId { get; set; } = default!;
    public bool? IsDeleted { get; set; }
}