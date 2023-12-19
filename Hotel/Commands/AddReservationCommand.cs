using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using Hotel.Application.DTOS.ReservationListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.MVVM.ViewModels.Modals;
using Hotel.Services.Interfaces;

namespace Hotel.Commands;

public class AddReservationCommand : BaseCommand
{
    private readonly INavigator _navigator;
    private readonly IReservationListingService _reservationListingService;
    private readonly AddReservationViewModel _reservationViewModel;


    public AddReservationCommand(INavigator navigator, AddReservationViewModel reservationViewModel,
        IReservationListingService reservationListingService)
    {
        _navigator = navigator;
        _reservationViewModel = reservationViewModel;
        _reservationListingService = reservationListingService;

        _reservationViewModel.PropertyChanged += OnModelPropertyChanged;
    }

    public override bool CanExecute(object? parameter)
    {
        return !string.IsNullOrEmpty(_reservationViewModel.FirstName) && base.CanExecute(parameter);
    }

    public override void Execute(object? parameter)
    {
        var addReservationDto = new AddReservationDto
        {
            CheckInDate = _reservationViewModel.CheckInDate,
            CheckOutDate = _reservationViewModel.CheckOutDate,
            TotalCost = _reservationViewModel.TotalCost,
            RoomId = _reservationViewModel.SelectedRoomId,
            GuestId = _reservationViewModel.SelectedGuestId
        };

        _reservationViewModel.PropertyChanged -= OnModelPropertyChanged;

        //TODO: Probably this isn't the best way to do that, Fire and forget?
        // _reservationListingService.CreateReservation(addReservationDto);
        if (addReservationDto is not null && !_reservationViewModel.HasErrors)
        {
            Task.Run(() => _reservationListingService.CreateReservation(addReservationDto));
            ;
            MessageBox.Show("Guest added!");
            _navigator.Close();
        }
        else
        {
            MessageBox.Show("Fill in the template!");
        }
    }

    private void OnModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(AddReservationViewModel.FirstName)) OnCanExecutedChanged();
    }
}