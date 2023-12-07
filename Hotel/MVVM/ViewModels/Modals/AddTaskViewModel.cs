using System;
using System.Linq;
using System.Windows.Input;
using Hotel.Application.DTOS;
using Hotel.Application.Services.Interfaces;
using Hotel.Commands;
using Hotel.Factories;
using Hotel.Services.Interfaces;

namespace Hotel.MVVM.ViewModels.Modals;

public class AddTaskViewModel : ViewModelBase
{
    private readonly ITaskService _taskService;
    
    public AddTaskViewModel(INavigator navigator, ITaskService taskService)
    {
        _taskService = taskService;

        GetAllMembers();

        CloseModal = new CloseModalCommand(navigator);
        AddTaskCommand = new AddTaskCommand(_taskService, this,navigator);
    }

    public ICommand CloseModal { get; set; }
    public ICommand AddTaskCommand { get;}

    private IQueryable<MyKeyValuePair> _allMembers;
    public IQueryable<MyKeyValuePair>  AllMembers
    {
        get { return _allMembers; }
        set
        {
            _allMembers = value;
            OnPropertyChanged(nameof(AllMembers));
        }
    }

    private string _description;
    public string Description
    {
        get { return _description; }
        set
        {
            _description = value;
            OnPropertyChanged(nameof(Description));
        }
    }

    private DateTime _dueTime = DateTime.Now;
    public DateTime DueTime
    {
        get { return _dueTime; }
        set
        {
            _dueTime = value;
            OnPropertyChanged(nameof(DueTime));
        }
    }

    private int _selectedMemberId;

    public int SelectedMemberId
    {
        get { return _selectedMemberId; }
        set
        {
            _selectedMemberId = value;
            OnPropertyChanged(nameof(SelectedMemberId));
        }
    }
    
    private void GetAllMembers()
    {
        AllMembers = _taskService.GetAllMembers().AsQueryable();
    }
}