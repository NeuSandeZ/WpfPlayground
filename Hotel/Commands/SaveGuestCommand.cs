using System.Threading.Tasks;
using System.Windows;
using Hotel.Application.DTOS.GuestsListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.MVVM.ViewModels.Modals;
using Hotel.Services.Interfaces;

namespace Hotel.Commands;

public class SaveGuestCommand : BaseCommand
{
    private readonly IGuestsListingService _guestsListingService;
    private readonly AddGuestViewModel _guestViewModel;
    private readonly INavigator _navigator;

    public SaveGuestCommand(INavigator navigator, AddGuestViewModel guestViewModel,
        IGuestsListingService guestsListingService)
    {
        _navigator = navigator;
        _guestViewModel = guestViewModel;
        _guestsListingService = guestsListingService;
    }

    public override void Execute(object? parameter)
    {
        var addGuestDto = new GuestDto
        {
            FirstName = _guestViewModel.FirstName,
            LastName = _guestViewModel.LastName,
            PhoneNumber = _guestViewModel.PhoneNumber,
            Email = _guestViewModel.Email,
            City = _guestViewModel.City,
            Street = _guestViewModel.Street,
            PostalCode = _guestViewModel.PostalCode
        };

        Task.Run(() => _guestsListingService.EditGuest(addGuestDto));
        _navigator.Close();
        MessageBox.Show("Successfully edited!", "Success", MessageBoxButton.OK);
    }
}