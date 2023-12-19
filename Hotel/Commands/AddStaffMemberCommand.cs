using System.Windows;
using Hotel.Application.DTOS.StaffListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.MVVM.ViewModels.Modals;
using Hotel.Services.Interfaces;

namespace Hotel.Commands;

public class AddStaffMemberCommand : BaseCommand
{
    private readonly AddStaffViewModel _addStaffViewModel;
    private readonly INavigator _navigator;
    private readonly IStaffService _staffService;

    public AddStaffMemberCommand(AddStaffViewModel addStaffViewModel, INavigator navigator, IStaffService staffService)
    {
        _addStaffViewModel = addStaffViewModel;
        _navigator = navigator;
        _staffService = staffService;
    }

    public override void Execute(object? parameter)
    {
        var staffMember = new CreateStaffMemberDto
        {
            FullName = $"{_addStaffViewModel.FirstName + " " + _addStaffViewModel.LastName}",
            Email = _addStaffViewModel.Email,
            PhoneNumber = _addStaffViewModel.PhoneNumber,
            City = _addStaffViewModel.City,
            Street = _addStaffViewModel.Street,
            PostalCode = _addStaffViewModel.PostalCode,
            StaffRoleId = _addStaffViewModel.SelectedRoleId
        };

        if (staffMember is not null && !_addStaffViewModel.HasErrors)
        {
            _staffService.CreateStaff(staffMember);
            MessageBox.Show("Guest added!");
            _navigator.Close();
        }
        else
        {
            MessageBox.Show("Fill in the template!");
        }
    }
}