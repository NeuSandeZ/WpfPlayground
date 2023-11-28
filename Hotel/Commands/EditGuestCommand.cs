using CommunityToolkit.Mvvm.Messaging;
using Hotel.Application.Services.Interfaces;
using Hotel.Factories;
using Hotel.MVVM.ViewModels;
using Hotel.MVVM.ViewModels.Modals;
using Hotel.Services.Interfaces;

namespace Hotel.Commands;

public class EditGuestCommand : BaseCommand, IRecipient<string>
{
    private readonly IGuestsListingService _guestsListingService;
    private readonly GuestViewModel _guestViewModel;
    private readonly INavigator _navigator;
    private readonly IViewModelFactory _viewModelFactory;

    public EditGuestCommand(INavigator navigator, IViewModelFactory viewModelFactory,
        IGuestsListingService guestsListingService, GuestViewModel guestViewModel)
    {
        _navigator = navigator;
        _viewModelFactory = viewModelFactory;
        _guestViewModel = guestViewModel;
        _guestsListingService = guestsListingService;
    }

    public void Receive(string m)
    {
        var guest = _guestsListingService.GetOneGuest(m);

        _navigator.CurrentModalViewModel = new AddGuestViewModel(_navigator, _guestsListingService)
        {
            FirstName = guest.FirstName,
            LastName = guest.LastName,
            Email = guest.Email,
            PhoneNumber = guest.PhoneNumber,
            City = guest.City,
            Street = guest.Street,
            PostalCode = guest.PostalCode,

            IsAddButtonVisible = false
        };
        WeakReferenceMessenger.Default.UnregisterAll(this);
    }

    public override void Execute(object? parameter)
    {
        WeakReferenceMessenger.Default.Register(this);
        _guestViewModel.SendGuestDto();
    }
}