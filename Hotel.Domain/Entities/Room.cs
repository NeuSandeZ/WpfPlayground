using System.ComponentModel.DataAnnotations;

namespace Hotel.Domain.Entities;

public class Room
{
    [Key] public int Id { get; set; }

    [Required] public int RoomNumber { get; set; }

    [Required] public int FloorNumber { get; set; }

    public int PricePerNight { get; set; }

    public RoomStatus? RoomStatus { get; set; } = default!;
    public int RoomStatusId { get; set; } = 3;

    public RoomType? RoomType { get; set; } = default!;
    public int? RoomTypeId { get; set; }

    public List<Reservation> Reservations { get; set; } = new();

    public bool? IsDeleted { get; set; }

    public RoomPromotions? RoomPromotions { get; set; }
    public int? RoomPromotionsId { get; set; }
}