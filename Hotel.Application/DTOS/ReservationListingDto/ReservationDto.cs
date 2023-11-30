namespace Hotel.Application.DTOS.ReservationListingDto;

public class ReservationDto
{
    public int ReservationId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public string TotalCost { get; set; }
    public string GuestFullName { get; set; }
    public string FloorAndRoomNumber { get; set; }

    public string ReservationStatus { get; set; }
    public string ReservationNumber { get; set; }
}