using System.ComponentModel.DataAnnotations;

namespace Hotel.Infrastructure.Entities;

public class RoomStatus
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string CurrentState { get; set; } = default!;
}