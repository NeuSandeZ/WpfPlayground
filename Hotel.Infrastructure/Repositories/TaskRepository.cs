using Hotel.Domain.Entities;
using Hotel.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using TaskStatus = Hotel.Domain.Entities.TaskStatus;

namespace Hotel.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly HotelDbContext _dbContext;
    
    public TaskRepository(HotelDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IEnumerable<Staff> GetAllMembers()
    {
        return _dbContext.Staves.Select(a => new Staff()
            {
                Id = a.Id,
                FullName = a.FullName
            }).AsNoTracking()
            .ToList();
    }

    public void CreateTask(Tasks task)
    {
        _dbContext.Add(task);
        _dbContext.SaveChanges();
    }

    public IEnumerable<Tasks> GetAllTasks()
    {
        return _dbContext.Tasks
            .Include(a => a.Staff)
            .Include(a => a.TaskStatus)
            .Select(a => new Tasks()
            {
                Description = a.Description,
                DueTime = a.DueTime,
                Staff = new Staff()
                {
                    FullName = a.Staff.FullName
                },
                TaskStatus = new TaskStatus()
                {
                    Status = a.TaskStatus.Status
                }
            }).AsNoTracking()
            .ToList();

    }
}