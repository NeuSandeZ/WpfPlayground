namespace Hotel.Application.DTOS.ReservationListingDto;

public class AddReservationDto
{
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public string TotalCost { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }

    public int ReservationStatusId { get; set; } = 4;
    public string ReservationNumber => new Random().Next(200000, 250000).ToString();

    public int RoomId { get; set; }
}