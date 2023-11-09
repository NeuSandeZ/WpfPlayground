using System;

namespace Hotel.MVVM.Models;

public class ReservationDto
{
    public string GuestFullName { get; set; }
    public string GuestPhoneNumber { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public int TotalCost { get; set; }
    public string FloorAndRoomNumber { get; set; }
}