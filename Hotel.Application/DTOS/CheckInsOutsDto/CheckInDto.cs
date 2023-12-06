using Hotel.Domain.Entities;

namespace Hotel.Application.DTOS.CheckInsOutsDto;

public class CheckInDto
{
    public DateTime CheckInDate { get; set; } = DateTime.Now;
    public int GuestId { get; set; }
    public int RoomId { get; set; }
    public int ReservationId { get; set; }
}