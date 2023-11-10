using System.ComponentModel.DataAnnotations;

namespace Hotel.Domain.Entities;

public class StaffRole
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Role { get; set; } = default!;
}