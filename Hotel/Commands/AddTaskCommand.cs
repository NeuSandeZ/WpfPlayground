using System.Threading.Tasks;
using Hotel.Application.DTOS.TasksListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.Domain.Entities;
using Hotel.MVVM.ViewModels.Modals;
using Hotel.Services.Interfaces;

namespace Hotel.Commands;

public class AddTaskCommand : BaseCommand
{
    private readonly ITaskService _taskService;
    private readonly AddTaskViewModel _addTaskViewModel;
    private readonly INavigator _navigator;
    public AddTaskCommand(ITaskService taskService, AddTaskViewModel addTaskViewModel, INavigator navigator)
    {
        _taskService = taskService;
        _addTaskViewModel = addTaskViewModel;
        _navigator = navigator;
    }
    public override void Execute(object? parameter)
    {
        var taskToAdd = new TasksAddDto()
        {
            Description = _addTaskViewModel.Description,
            DueTime = _addTaskViewModel.DueTime,
            StaffId = _addTaskViewModel.SelectedMemberId
        };
        
        //TODO F&F
        Task.Run(() => _taskService.CreateTask(taskToAdd));
        _navigator.Close();
    }
}