namespace Hotel.Application.DTOS.PaymentDto;

public class PaymentDto
{
    public DateTime PaymentDate { get; set; } 
    public int Amount { get; set; }
    public int PaymentTypeId { get; set; }
    public int ReservationId { get; set; }
}