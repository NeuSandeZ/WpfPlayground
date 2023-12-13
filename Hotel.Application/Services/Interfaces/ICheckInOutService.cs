using Hotel.Application.DTOS.CheckInsOutsDto;
using Hotel.Application.DTOS.GuestsListingDto;

namespace Hotel.Application.Services.Interfaces;

public interface ICheckInOutService
{
    Task<int> GetTodaysCheckIns();
    Task<int> GetTodaysCheckOuts();
    Task<IEnumerable<ReservationComboBoxDto>> GetAllReservationNumbers();
    Task<IEnumerable<CheckInListingDto>> GetAllCheckIns();

    void CreateCheckIn(CheckInDto checkInDto);
    
    void CheckOut(CheckOutDto selectedCheckOut);
}