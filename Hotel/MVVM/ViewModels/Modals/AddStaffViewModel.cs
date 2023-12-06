using System.Linq;
using System.Windows.Input;
using Hotel.Application.DTOS.StaffListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.Commands;
using Hotel.Services.Interfaces;

namespace Hotel.MVVM.ViewModels.Modals;

public class AddStaffViewModel : ViewModelBase
{
    private readonly IStaffService _staffService;
    
    private string _firstName;
    
    private string _lastName;
    
    private string _totalCost;
    
    private string _email;
    
    private string _city;
    
    private string _street;
    
    private string _postalCode;
    
    private string _phoneNumber;

    public AddStaffViewModel(IStaffService staffService, INavigator navigator)
    {
        _staffService = staffService;
        GetAllRoles();

        CloseModal = new CloseModalCommand(navigator);
        AddStaffMemberCommand = new AddStaffMemberCommand(this,navigator,staffService);
    }

    public ICommand CloseModal { get; set; }
    public ICommand AddStaffMemberCommand { get; set; }
    public string FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;
            OnPropertyChanged(nameof(FirstName));
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;
            OnPropertyChanged(nameof(LastName));
        }
    }

    public string Email
    {
        get { return _email; }
        set
        {
            _email = value;
            OnPropertyChanged(nameof(Email));
        }
    }
    
    public string PhoneNumber
    {
        get { return _phoneNumber; }
        set
        {
            _phoneNumber = value;
            OnPropertyChanged(nameof(PhoneNumber));
        }
    }
    
    public string City
    {
        get { return _city; }
        set
        {
            _city = value;
            OnPropertyChanged(nameof(City));
        }
    }
    
    public string Street
    {
        get { return _street; }
        set
        {
            _street = value;
            OnPropertyChanged(nameof(Street));
        }
    }
    
    public string PostalCode
    {
        get { return _postalCode; }
        set
        {
            _postalCode = value;
            OnPropertyChanged(nameof(PostalCode));
        }
    }

    private int _selectedRoleId;

    public int SelectedRoleId
    {
        get { return _selectedRoleId; }
        set
        {
            _selectedRoleId = value;
            OnPropertyChanged(nameof(SelectedRoleId));
        }
    }

    private IQueryable<RolesDto> _rolesComboBox;

    public IQueryable<RolesDto> RolesComboBox
    {
        get { return _rolesComboBox; }
        set
        {
            _rolesComboBox = value;
            OnPropertyChanged(nameof(RolesComboBox));
        }
    }

    private void GetAllRoles()
    {
        RolesComboBox = _staffService.GetAllStaffRoles().AsQueryable();
    }
}