using System.ComponentModel.DataAnnotations;

namespace Hotel.Infrastructure.Entities;

public class Room
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int RoomNumber { get; set; }
    [Required]
    public int FloorNumber { get; set; }
    public bool IsAvailable { get; set; }
    
    public RoomStatus RoomStatus { get; set; } = default!;
    public int RoomStatusId { get; set; }

    public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    
    public bool? IsDeleted { get; set; }
}