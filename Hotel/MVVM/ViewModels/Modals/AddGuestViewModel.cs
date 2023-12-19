using System.Text.RegularExpressions;
using System.Windows.Input;
using Hotel.Application.Services.Interfaces;
using Hotel.Commands;
using Hotel.Services.Interfaces;

namespace Hotel.MVVM.ViewModels.Modals;

public class AddGuestViewModel : ViewModelBaseWithINotify
{
    private string _city;

    private string _email;

    private string _firstName;

    private string _lastName;

    private string _phoneNumber;

    private string _postalCode;

    private string _street;

    public AddGuestViewModel(INavigator navigator, IGuestsListingService guestsListingService)
    {
        AddGuestCommand = new AddGuestCommand(navigator, this, guestsListingService);
        SaveGuestCommand = new SaveGuestCommand(navigator, this, guestsListingService);
        CloseModal = new CloseModalCommand(navigator);
    }

    public string FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;
            OnPropertyChanged();

            ClearErrors(nameof(FirstName));
            if (string.IsNullOrWhiteSpace(value))
            {
                AddError("First name cannot be empty or null.", nameof(FirstName));
                OnErrorsChanged(nameof(FirstName));
            }
            else if (!Regex.IsMatch(value, "^[a-zA-Z]+$"))
            {
                AddError("First name must contain only letters.", nameof(FirstName));
                OnErrorsChanged(nameof(FirstName));
            }
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;
            OnPropertyChanged();

            ClearErrors(nameof(LastName));
            if (string.IsNullOrWhiteSpace(value))
            {
                AddError("First name cannot be empty or null.", nameof(LastName));
                OnErrorsChanged(nameof(LastName));
            }
            else if (!Regex.IsMatch(value, "^[a-zA-Z]+$"))
            {
                AddError("First name must contain only letters.", nameof(LastName));
                OnErrorsChanged(nameof(LastName));
            }
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged();

            ClearErrors(nameof(Email));
            if (string.IsNullOrWhiteSpace(value))
            {
                AddError("Email cannot be empty or null.", nameof(Email));
                OnErrorsChanged(nameof(Email));
            }
            else if (!Regex.IsMatch(value, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                AddError("Invalid email format.", nameof(Email));
                OnErrorsChanged(nameof(Email));
            }
        }
    }

    public string PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            _phoneNumber = value;
            OnPropertyChanged();

            ClearErrors(nameof(PhoneNumber));
            if (string.IsNullOrWhiteSpace(value))
            {
                AddError("Phone number cannot be empty or null.", nameof(PhoneNumber));
                OnErrorsChanged(nameof(PhoneNumber));
            }
            else if (!Regex.IsMatch(value, @"^\+[0-9]{1,4}[0-9]{6,}$"))
            {
                AddError(
                    "Invalid phone number format. It should start with a '+' followed by country code and the actual number.",
                    nameof(PhoneNumber));
                OnErrorsChanged(nameof(PhoneNumber));
            }
        }
    }

    public string City
    {
        get => _city;
        set
        {
            _city = value;
            OnPropertyChanged();

            ClearErrors(nameof(City));
            if (string.IsNullOrWhiteSpace(value))
            {
                AddError("City cannot be empty or null.", nameof(City));
                OnErrorsChanged(nameof(City));
            }
        }
    }

    public string Street
    {
        get => _street;
        set
        {
            _street = value;
            OnPropertyChanged();

            ClearErrors(nameof(Street));
            if (string.IsNullOrWhiteSpace(value))
            {
                AddError("Street cannot be empty or null.", nameof(Street));
                OnErrorsChanged(nameof(Street));
            }
        }
    }

    public string PostalCode
    {
        get => _postalCode;
        set
        {
            _postalCode = value;
            OnPropertyChanged();

            ClearErrors(nameof(PostalCode));
            if (string.IsNullOrWhiteSpace(value))
            {
                AddError("Street cannot be empty or null.", nameof(PostalCode));
                OnErrorsChanged(nameof(PostalCode));
            }
            else if (!Regex.IsMatch(value, @"^\d{2}-\d{3}$"))
            {
                AddError("Invalid postal code format. It should be in the format [2 numbers]-[3 numbers].",
                    nameof(PostalCode));
                OnErrorsChanged(nameof(PostalCode));
            }
        }
    }

    public bool IsAddButtonVisible { get; set; } = true;

    public ICommand AddGuestCommand { get; }
    public ICommand SaveGuestCommand { get; }
    public ICommand CloseModal { get; }
}