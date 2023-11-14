using System.ComponentModel.DataAnnotations;

namespace Hotel.Domain.Entities;

public class PaymentType
{
    [Key] public int Id { get; set; }

    [Required] public string PaymentMethod { get; set; } = default!;
}