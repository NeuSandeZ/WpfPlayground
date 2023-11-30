namespace Hotel.Application.DTOS.PaymentDto;

public class PaymentListingDto
{
    public string Amount { get; set; }
    public string PaymentDate { get; set; }
    public string PaymentMethod { get; set; }
    public string ReservationNumber { get; set; }
}