using Hotel.Domain.Entities;

namespace Hotel.Application.DTOS.ReservationListingDto;

public class AddReservationDto
{
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public string TotalCost { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public int RoomId { get; set; }
}