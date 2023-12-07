using AutoMapper;
using Hotel.Application.DTOS;
using Hotel.Application.DTOS.TasksListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.Domain.Entities;
using Hotel.Domain.IRepositories;

namespace Hotel.Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;
    
    public TaskService(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public IEnumerable<MyKeyValuePair> GetAllMembers()
    {
        var allMembers = _taskRepository.GetAllMembers();

        return allMembers.Select(a => new MyKeyValuePair { Key = a.Id, Value = a.FullName }).ToList();
    }

    public void CreateTask(TasksAddDto tasksAddDto)
    {
        var task = _mapper.Map<Tasks>(tasksAddDto);
        _taskRepository.CreateTask(task);
    }

    public IEnumerable<TasksListingDto> GetAllTasks()
    {
        var tasksEnumerable = _taskRepository.GetAllTasks();
        return _mapper.Map<IEnumerable<TasksListingDto>>(tasksEnumerable);
    }
}