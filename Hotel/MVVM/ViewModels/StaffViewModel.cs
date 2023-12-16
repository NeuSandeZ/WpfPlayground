using System.Collections.ObjectModel;
using System.Windows.Input;
using Hotel.Application.DTOS.StaffListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.Commands;
using Hotel.Factories;
using Hotel.Services.Interfaces;

namespace Hotel.MVVM.ViewModels;

public class StaffViewModel : ViewModelBase
{
    private readonly INavigator _navigator;
    private readonly IViewModelFactory _viewModelFactory;
    private readonly IStaffService _staffService;

    public StaffViewModel(INavigator navigator, IViewModelFactory viewModelFactory, IStaffService staffService)
    {
        _navigator = navigator;
        _viewModelFactory = viewModelFactory;
        _staffService = staffService;

        GetAllStaffMembers();
        
        OpenModal = new OpenModalCommand(navigator, viewModelFactory, () => ViewType.AddStaff);
        Refresh = new ActionBaseCommand(() => GetAllStaffMembers());
    }

    public ICommand OpenModal { get; set; }
    public ICommand Refresh { get; set; }

    private ObservableCollection<StaffListingDto> _staffListing;

    public ObservableCollection<StaffListingDto>  StaffListing
    {
        get { return _staffListing; }
        set
        {
            _staffListing = value;
            OnPropertyChanged(nameof(StaffListing));
        }
    }
    private void GetAllStaffMembers()
    {
        var staffListingDtos = _staffService.GetAllStaffMembers();
        StaffListing = new ObservableCollection<StaffListingDto>(staffListingDtos);
    }
}