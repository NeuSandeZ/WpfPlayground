using Hotel.Domain.Entities;

namespace Hotel.Domain.IRepositories;

public interface ITaskRepository
{
    IEnumerable<Staff> GetAllMembers();
    void CreateTask(Tasks task);

    IEnumerable<Tasks> GetAllTasks();
}