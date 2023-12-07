namespace Hotel.Application.DTOS.TasksListingDto;

public class TasksAddDto
{
    public string Description { get; set; } 
    public DateTime DueTime { get; set; }
    public int StaffId { get; set; }
    public int TaskStatusId { get; set; } = 1;
}