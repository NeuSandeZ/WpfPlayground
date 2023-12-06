using System.ComponentModel.DataAnnotations;

namespace Hotel.Domain.Entities;

public class CheckIns
{
    [Key] public int Id { get; set; }
    public DateTime CheckInDate { get; set; } = DateTime.Now;
    public Guest Guest { get; set; }
    public int GuestId { get; set; }
    public Room Room { get; set; }
    public int RoomId { get; set; }
    public Reservation Reservation { get; set; }
    public int ReservationId { get; set; }

    public CheckOuts? CheckOuts { get; set; }
    public int? CheckOutsId { get; set; }
}