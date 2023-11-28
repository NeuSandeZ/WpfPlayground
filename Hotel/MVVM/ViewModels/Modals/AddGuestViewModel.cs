using System.Windows.Input;
using Hotel.Application.Services.Interfaces;
using Hotel.Commands;
using Hotel.Services.Interfaces;

namespace Hotel.MVVM.ViewModels.Modals;

public class AddGuestViewModel : ViewModelBase
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
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;
            OnPropertyChanged();
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged();
        }
    }

    public string PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            _phoneNumber = value;
            OnPropertyChanged();
        }
    }

    public string City
    {
        get => _city;
        set
        {
            _city = value;
            OnPropertyChanged();
        }
    }

    public string Street
    {
        get => _street;
        set
        {
            _street = value;
            OnPropertyChanged();
        }
    }

    public string PostalCode
    {
        get => _postalCode;
        set
        {
            _postalCode = value;
            OnPropertyChanged();
        }
    }

    public bool IsAddButtonVisible { get; set; } = true;

    public ICommand AddGuestCommand { get; }
    public ICommand SaveGuestCommand { get; }
    public ICommand CloseModal { get; }
}