using System.ComponentModel.DataAnnotations;

namespace Hotel.Infrastructure.Entities;

public class Tasks
{
    [Key]
    public int Id { get; set; }

    public string Description { get; set; } = default!;
    public DateTime DueTime { get; set; }

    public bool IsCompleted { get; set; }

    public Staff Staff { get; set; } = default!;
    public int StaffId { get; set; }
}