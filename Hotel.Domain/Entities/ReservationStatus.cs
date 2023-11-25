using System.ComponentModel.DataAnnotations;

namespace Hotel.Domain.Entities;

public class ReservationStatus
{
    [Key] public int Id { get; set; }

    [Required] public string ReservationsStatus { get; set; } = default!;
}