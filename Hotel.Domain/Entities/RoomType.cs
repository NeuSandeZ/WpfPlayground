using System.ComponentModel.DataAnnotations;

namespace Hotel.Domain.Entities;

public class RoomType
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Type { get; set; } = default!;
}