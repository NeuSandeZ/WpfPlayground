using System.ComponentModel.DataAnnotations;

namespace Hotel.Domain.Entities;

public class Tasks
{
    [Key] public int Id { get; set; }

    public string Description { get; set; } = default!;
    public DateTime DueTime { get; set; }

    public TaskStatus TaskStatus { get; set; }
    public int TaskStatusId { get; set; }

    public Staff Staff { get; set; } = default!;
    public int StaffId { get; set; }
}