using Hotel.Domain.Entities;

namespace Hotel.Domain.IRepositories;

public interface ICheckInOutRepository
{
    IEnumerable<Reservation> GetAllReservationNumbers();
    public int GetTodaysCheckIns();
    public int GetTodaysCheckOuts();
    void CreateCheckIn(CheckIns checkIns);
    IEnumerable<CheckIns> GetAllCheckIns();
    void CheckOut(CheckOuts checkOuts);
}