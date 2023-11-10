using System.ComponentModel.DataAnnotations;

namespace Hotel.Domain.Entities;

public class Payment
{
    [Key]
    public int Id { get; set; }
    public DateTime PaymentDate { get; set; }
    public int Amount { get; set; }

    public PaymentType PaymentType { get; set; } = default!;
    public int PaymentTypeId { get; set; }
}