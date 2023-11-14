using System.Collections.ObjectModel;
using Hotel.Domain.Entities;

namespace Hotel.Domain.IRepositories;

public interface IReservationListingRepository
{
    Task<IEnumerable<Reservation>> GetAll();
}