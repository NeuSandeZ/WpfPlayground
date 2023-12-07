using System.ComponentModel.DataAnnotations;

namespace Hotel.Domain.Entities;

public class RoomPromotions
{
    [Key] public int Id { get; set; }
    public string DiscountAmount { get; set; }

    public Room Room { get; set; }
    public int RoomId { get; set; }
}