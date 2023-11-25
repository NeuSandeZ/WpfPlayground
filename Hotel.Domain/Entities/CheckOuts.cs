using System.ComponentModel.DataAnnotations;

namespace Hotel.Domain.Entities;

public class CheckOuts
{
    [Key] public int Id { get; set; }
    public DateTime CheckOutDate { get; set; }
    public Guest Guest { get; set; }
    public int GuestId { get; set; }
    public Room Room { get; set; }
    public int RoomId { get; set; }
}