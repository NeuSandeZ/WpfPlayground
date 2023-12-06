using Hotel.Application.DTOS.CheckInsOutsDto;
using Hotel.Application.DTOS.GuestsListingDto;

namespace Hotel.Application.Services.Interfaces;

public interface ICheckInOutService
{
    int GetTodaysCheckIns();
    int GetTodaysCheckOuts();
    IEnumerable<ReservationComboBoxDto> GetAllReservationNumbers();
    void CreateCheckIn(CheckInDto checkInDto);
    IEnumerable<CheckInListingDto> GetAllCheckIns();
    
    void CheckOut(CheckOutDto selectedCheckOut);
}