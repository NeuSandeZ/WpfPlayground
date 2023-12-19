using System;
using System.Linq;
using System.Windows.Input;
using Hotel.Application.DTOS;
using Hotel.Application.Services.Interfaces;
using Hotel.Commands;
using Hotel.Services.Interfaces;

namespace Hotel.MVVM.ViewModels.Modals;

public class AddTaskViewModel : ViewModelBaseWithINotify
{
    private readonly ITaskService _taskService;

    private IQueryable<MyKeyValuePair> _allMembers;

    private string _description;

    private DateTime _dueTime = DateTime.Now;

    private int _selectedMemberId;

    public AddTaskViewModel(INavigator navigator, ITaskService taskService)
    {
        _taskService = taskService;

        GetAllMembers();

        CloseModal = new CloseModalCommand(navigator);
        AddTaskCommand = new AddTaskCommand(_taskService, this, navigator);
    }

    public ICommand CloseModal { get; set; }
    public ICommand AddTaskCommand { get; }

    public IQueryable<MyKeyValuePair> AllMembers
    {
        get => _allMembers;
        set
        {
            _allMembers = value;
            OnPropertyChanged();
        }
    }

    public string Description
    {
        get => _description;
        set
        {
            _description = value;
            OnPropertyChanged();

            ClearErrors(nameof(Description));
            if (string.IsNullOrWhiteSpace(value))
            {
                AddError("Description cannot be empty or null.", nameof(Description));
                OnErrorsChanged(nameof(Description));
            }
        }
    }

    public DateTime DueTime
    {
        get => _dueTime;
        set
        {
            _dueTime = value;
            OnPropertyChanged();

            ClearErrors(nameof(DueTime));
            if (DueTime < DateTime.Now)
            {
                AddError("I want things to be done yesterday as well, but you need to choose a future date ;)",
                    nameof(DueTime));
                OnErrorsChanged(nameof(DueTime));
            }
        }
    }

    public int SelectedMemberId
    {
        get => _selectedMemberId;
        set
        {
            _selectedMemberId = value;
            OnPropertyChanged();
        }
    }

    private void GetAllMembers()
    {
        AllMembers = _taskService.GetAllMembers().AsQueryable();
    }
}