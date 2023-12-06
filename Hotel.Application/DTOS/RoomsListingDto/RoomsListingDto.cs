namespace Hotel.Application.DTOS.RoomsListingDto;

public class RoomsListingDto
{
    public string RoomNumber { get; set; }
    public string FloorNumber { get; set; }
    public string PricePerNight { get; set; }
    public string RoomStatus { get; set; }
    public string RoomType { get; set; }

    public int SelectedRoomTypeId { get; set; }
}