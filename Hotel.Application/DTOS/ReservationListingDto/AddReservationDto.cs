namespace Hotel.Application.DTOS.ReservationListingDto;

public class AddReservationDto
{
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public string TotalCost { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string PostalCode { get; set; }

    public int ReservationStatusId { get; set; } = 1;
    public string ReservationNumber => new Random().Next(200000, 250000).ToString();

    public int RoomId { get; set; }
    public int GuestId { get; set; }
}