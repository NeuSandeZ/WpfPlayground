using System.ComponentModel.DataAnnotations;

namespace Hotel.Infrastructure.Entities;

public class PaymentType
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string PaymentMethod { get; set; } = default!;
}