using System.ComponentModel.DataAnnotations;

namespace Hotel.Domain.Entities;

public class TaskStatus
{
    [Key] public int Id { get; set; }
    [Required] public string Status { get; set; }
}