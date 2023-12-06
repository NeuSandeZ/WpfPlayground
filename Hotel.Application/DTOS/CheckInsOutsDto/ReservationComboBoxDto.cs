namespace Hotel.Application.DTOS.CheckInsOutsDto;

public class ReservationComboBoxDto
{
    public int ReservationId { get; set; }
    public int GuestId { get; set; }
    public int RoomId { get; set; }

    public string FloorAndRoomNumber { get; set; }
    public string ReservationNumber { get; set; }
}