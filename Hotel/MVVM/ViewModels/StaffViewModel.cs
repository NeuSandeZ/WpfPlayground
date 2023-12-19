using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Hotel.Application.DTOS.StaffListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.Commands;
using Hotel.Factories;
using Hotel.Services.Interfaces;

namespace Hotel.MVVM.ViewModels;

public class StaffViewModel : SortingAndFilteringViewModel<StaffListingDto>
{
    private readonly INavigator _navigator;
    private readonly IStaffService _staffService;
    private readonly IViewModelFactory _viewModelFactory;

    public StaffViewModel(INavigator navigator, IViewModelFactory viewModelFactory, IStaffService staffService)
    {
        _navigator = navigator;
        _viewModelFactory = viewModelFactory;
        _staffService = staffService;

        GetAllStaffMembers();

        FilterComboBoxList = new ObservableCollection<string>(LoadFilterComboBoxList());
        SortComboBoxList = new ObservableCollection<string>(LoadSortComboBoxList());

        OpenModal = new OpenModalCommand(navigator, viewModelFactory, () => ViewType.AddStaff);
        Refresh = new ActionBaseCommand(() => GetAllStaffMembers());
    }

    public ICommand OpenModal { get; set; }
    public ICommand Refresh { get; set; }

    protected override Dictionary<string, Func<StaffListingDto, string>> FilterByColumn { get; } = new()
    {
        { "Fullname", a => a.FullName },
        { "Email", a => a.Email },
        { "Role", a => a.Role },
        { "Phone number", a => a.PhoneNumber },
        { "City", a => a.City },
        { "Street", a => a.Street },
        { "Postal code", a => a.PostalCode }
    };

    ~StaffViewModel()
    {
        DisposeFilter();
    }

    private void GetAllStaffMembers()
    {
        var staffListingDtos = _staffService.GetAllStaffMembers();
        Items = new ObservableCollection<StaffListingDto>(staffListingDtos);
    }

    protected override List<string> LoadFilterComboBoxList()
    {
        return new List<string>
        {
            "Fullname",
            "Email",
            "Role",
            "Phone number",
            "City",
            "Street",
            "Postal code"
        };
    }

    protected override void Sort()
    {
        CollectionView.SortDescriptions.Clear();

        var columnToSort = ChoosenSortField switch
        {
            "Fullname" => nameof(StaffListingDto.FullName),
            "Email" => nameof(StaffListingDto.Email),
            "Role" => nameof(StaffListingDto.Role),
            "Phone number" => nameof(StaffListingDto.PhoneNumber),
            "City" => nameof(StaffListingDto.City),
            "Street" => nameof(StaffListingDto.Street),
            "Postal code" => nameof(StaffListingDto.PostalCode),
            _ => null
        };
        SortByAscOrDesc(columnToSort);
    }
}