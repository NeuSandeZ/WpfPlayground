using System.ComponentModel.DataAnnotations;

namespace Hotel.Infrastructure.Entities;

public class Address
{
    [Key]
    public int Id { get; set; }
    public string? City { get; set; } = default!;
    public string? Street { get; set; } = default!;
    public string? PostalCode { get; set; } = default!;
    
    public bool? IsDeleted { get; set; }

}