using System.Linq;
using System.Threading.Tasks;
using Hotel.Application.DTOS.ReservationListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.MVVM.ViewModels;
using Hotel.MVVM.ViewModels.Modals;
using Hotel.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Hotel.Commands;

public class AddReservationCommand : BaseCommand
{
    private readonly INavigator _navigator;
    private readonly AddReservationViewModel _reservationViewModel;
    private readonly IReservationListingService _reservationListingService;

    public AddReservationCommand(INavigator navigator, AddReservationViewModel reservationViewModel, IReservationListingService reservationListingService)
    {
        _navigator = navigator;
        _reservationViewModel = reservationViewModel;
        _reservationListingService = reservationListingService;
    }
    
     public override void Execute(object? parameter)
     {
         var addReservationDto = new AddReservationDto()
         {
             CheckInDate = _reservationViewModel.CheckInDate,
             CheckOutDate = _reservationViewModel.CheckOutDate,
             FirstName = _reservationViewModel.FirstName,
             LastName = _reservationViewModel.LastName,
             TotalCost = _reservationViewModel.TotalCost,
             RoomId = _reservationViewModel.SelectedRoomId
         };
         
         //TODO: Probably this isn't the best way to do that, Fire and forget?
         Task.Run(() => _reservationListingService.CreateReservation(addReservationDto));
         
         // _reservationListingService.CreateReservation(addReservationDto);
         _navigator.Close();
     }
}