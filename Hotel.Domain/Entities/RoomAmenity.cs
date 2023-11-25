using System.ComponentModel.DataAnnotations;

namespace Hotel.Domain.Entities;

public class RoomAmenity
{
    [Key] public int Id { get; set; }
    public string Amenity { get; set; }
    public string Description { get; set; }
}