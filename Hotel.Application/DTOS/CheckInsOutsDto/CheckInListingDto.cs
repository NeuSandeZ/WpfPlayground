namespace Hotel.Application.DTOS.CheckInsOutsDto;

public class CheckInListingDto
{
    public int CheckInId { get; set; }
    public string FloorAndRoomNumber { get; set; }
    public string ReservationNumber { get; set; }
    public string FullGuestName { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime? CheckOutDate { get; set; }
}