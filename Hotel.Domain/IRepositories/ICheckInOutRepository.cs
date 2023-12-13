using Hotel.Domain.Entities;

namespace Hotel.Domain.IRepositories;

public interface ICheckInOutRepository
{
    Task<IEnumerable<Reservation>> GetAllReservationNumbers();
    Task<int> GetTodaysCheckIns();
    Task<int> GetTodaysCheckOuts();
    Task<IEnumerable<CheckIns>> GetAllCheckIns();

    void CreateCheckIn(CheckIns checkIns);
    void CheckOut(CheckOuts checkOuts);
}