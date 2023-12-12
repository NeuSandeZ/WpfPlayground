using System.ComponentModel.DataAnnotations;

namespace Hotel.Domain.Entities;

public class Reservation
{
    [Key] public int Id { get; set; }

    [Required] public DateTime CheckInDate { get; set; }

    [Required] public DateTime CheckOutDate { get; set; }

    public int? TotalCost { get; set; }

    public Room Room { get; set; } = default!;
    public int RoomId { get; set; }
    public Guest Guest { get; set; } = default!;
    public int GuestId { get; set; }

    public ReservationStatus? ReservationStatus { get; set; } = default!;
    public int? ReservationStatusId { get; set; }

    public bool IsCheckedIn { get; set; } = false;
    public bool IsCheckedOut { get; set; } = false;
    [Required] public string ReservationNumber { get; set; }

    public bool? IsDeleted { get; set; }
}