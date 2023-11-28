using System.ComponentModel.DataAnnotations;

namespace Hotel.Domain.Entities;

public class ReservationStatus
{
    [Key] public int Id { get; set; }

    public string Status { get; set; } = default!;
}