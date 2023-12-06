namespace Hotel.Application.DTOS.StaffListingDto;

public class StaffListingDto
{
    public string FullName { get; set; } = default!;

    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string Role { get; set; }
    public string City { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string PostalCode { get; set; } = default!;
}