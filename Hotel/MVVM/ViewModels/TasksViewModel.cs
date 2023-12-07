using System.Collections.ObjectModel;
using System.Windows.Input;
using Hotel.Application.DTOS.TasksListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.Commands;
using Hotel.Factories;
using Hotel.Services.Interfaces;

namespace Hotel.MVVM.ViewModels;

public class TasksViewModel : ViewModelBase
{
    private readonly INavigator _navigator;
    private readonly IViewModelFactory _viewModelFactory;
    private readonly ITaskService _taskService;
    
    public TasksViewModel(INavigator navigator, IViewModelFactory viewModelFactory, ITaskService taskService)
    {
        _navigator = navigator;
        _viewModelFactory = viewModelFactory;
        _taskService = taskService;

        GetAllTasks();

        OpenModal = new OpenModalCommand(navigator, viewModelFactory, () => ViewType.AddTask);
    }

    public ICommand OpenModal { get;}

    private ObservableCollection<TasksListingDto> _allTasks;

    public ObservableCollection<TasksListingDto> AllTasks
    {
        get { return _allTasks; }
        set
        {
            _allTasks = value;
            OnPropertyChanged(nameof(AllTasks));
        }
    }
    private void GetAllTasks()
    {
        var tasksListingDtos = _taskService.GetAllTasks();
        AllTasks = new ObservableCollection<TasksListingDto>(tasksListingDtos);
    }
}