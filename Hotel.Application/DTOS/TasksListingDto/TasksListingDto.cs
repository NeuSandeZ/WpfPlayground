namespace Hotel.Application.DTOS.TasksListingDto;

public class TasksListingDto
{
    public string Description { get; set; } = default!;
    public string Status { get; set; } = default!;
    public string AssignedTo { get; set; } = default!;
    public DateTime DueTime { get; set; }
}