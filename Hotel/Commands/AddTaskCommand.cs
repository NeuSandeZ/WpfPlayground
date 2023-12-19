using System.Threading.Tasks;
using System.Windows;
using Hotel.Application.DTOS.TasksListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.MVVM.ViewModels.Modals;
using Hotel.Services.Interfaces;

namespace Hotel.Commands;

public class AddTaskCommand : BaseCommand
{
    private readonly AddTaskViewModel _addTaskViewModel;
    private readonly INavigator _navigator;
    private readonly ITaskService _taskService;

    public AddTaskCommand(ITaskService taskService, AddTaskViewModel addTaskViewModel, INavigator navigator)
    {
        _taskService = taskService;
        _addTaskViewModel = addTaskViewModel;
        _navigator = navigator;
    }

    public override void Execute(object? parameter)
    {
        var taskToAdd = new TasksAddDto
        {
            Description = _addTaskViewModel.Description,
            DueTime = _addTaskViewModel.DueTime,
            StaffId = _addTaskViewModel.SelectedMemberId
        };

        //TODO F&F
        if (taskToAdd is not null && !_addTaskViewModel.HasErrors)
        {
            Task.Run(() => _taskService.CreateTask(taskToAdd));
            ;
            MessageBox.Show("Guest added!");
            _navigator.Close();
        }
        else
        {
            MessageBox.Show("Fill in the template!");
        }
    }
}