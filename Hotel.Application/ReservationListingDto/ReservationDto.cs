namespace Hotel.Application.ReservationListingDto;

public class ReservationDto
{
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public int? TotalCost { get; set; }

    public string GuestFullName { get; set; }
    public string FloorAndRoomNumber { get; set; }
}