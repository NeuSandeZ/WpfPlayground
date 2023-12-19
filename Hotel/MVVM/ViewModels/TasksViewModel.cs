using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Hotel.Application.DTOS.TasksListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.Commands;
using Hotel.Factories;
using Hotel.Services.Interfaces;

namespace Hotel.MVVM.ViewModels;

public class TasksViewModel : SortingAndFilteringViewModel<TasksListingDto>
{
    private readonly INavigator _navigator;
    private readonly ITaskService _taskService;
    private readonly IViewModelFactory _viewModelFactory;

    public TasksViewModel(INavigator navigator, IViewModelFactory viewModelFactory, ITaskService taskService)
    {
        _navigator = navigator;
        _viewModelFactory = viewModelFactory;
        _taskService = taskService;

        GetAllTasks();

        FilterComboBoxList = new ObservableCollection<string>(LoadFilterComboBoxList());
        SortComboBoxList = new ObservableCollection<string>(LoadSortComboBoxList());

        OpenModal = new OpenModalCommand(navigator, viewModelFactory, () => ViewType.AddTask);
        Refresh = new ActionBaseCommand(() => GetAllTasks());
    }

    public ICommand OpenModal { get; }
    public ICommand Refresh { get; }

    protected override Dictionary<string, Func<TasksListingDto, string>> FilterByColumn { get; } = new()
    {
        { "Description", a => a.Description },
        { "Status", a => a.Status },
        { "Assigned to", a => a.AssignedTo },
        { "Due time", a => a.DueTime.ToString() }
    };

    ~TasksViewModel()
    {
        DisposeFilter();
    }


    private void GetAllTasks()
    {
        var tasksListingDtos = _taskService.GetAllTasks();
        Items = new ObservableCollection<TasksListingDto>(tasksListingDtos);
    }

    protected override List<string> LoadFilterComboBoxList()
    {
        return new List<string>
        {
            "Description",
            "Status",
            "Assigned to",
            "Due time"
        };
    }

    protected override void Sort()
    {
        CollectionView.SortDescriptions.Clear();

        var columnToSort = ChoosenSortField switch
        {
            "Description" => nameof(TasksListingDto.Description),
            "Status" => nameof(TasksListingDto.Status),
            "Assigned to" => nameof(TasksListingDto.AssignedTo),
            "Due time" => nameof(TasksListingDto.DueTime),
            _ => null
        };
        SortByAscOrDesc(columnToSort);
    }
}