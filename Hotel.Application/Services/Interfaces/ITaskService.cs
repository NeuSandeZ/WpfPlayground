using Hotel.Application.DTOS;
using Hotel.Application.DTOS.TasksListingDto;

namespace Hotel.Application.Services.Interfaces;

public interface ITaskService
{
    IEnumerable<MyKeyValuePair> GetAllMembers();
    void CreateTask(TasksAddDto tasksAddDto);

    IEnumerable<TasksListingDto> GetAllTasks();
}